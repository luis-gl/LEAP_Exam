using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private Transform playerTextTransform;
    private TextMeshProUGUI playerText;
    private string textToShow;

    [Header("Movement Variables")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpSpeed;
    private float horizontal;
    private float vertical;
    private bool isRunning;
    private bool isGrounded;
    private bool isFalling;
    [SerializeField] private bool isRolling;

    [Header("Combo Variables")]
    public int timesMeleeKeyPressed;
    public int timesRangeKeyPressed;
    public float maxComboDelay;
    private float lastMeleeKeyPressed;
    private float lastRangeKeyPressed;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask floorLayerMask;

    [Header("Variables for Fix")]
    private Vector3 rightScale;
    private Vector3 leftScale;

    [Header("Components")]
    private Rigidbody2D myRigidBody;
    private BoxCollider2D myCollider;
    private Animator myAnimator;
    
    [Header("Animator Properties Hashes")]
    private int groundedAnimProp;
    private int movingAnimProp;
    private int fallingAnimProp;
    private int rollAnimProp;
    private int meleeAttack1AnimProp;
    private int meleeAttack2AnimProp;
    private int meleeAttack3AnimProp;
    private int rangeAttack1AnimProp;
    private int rangeAttack2AnimProp;

    // Start is called before the first frame update
    void Start()
    {
        playerText = playerTextTransform.GetComponent<TextMeshProUGUI>();

        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();

        rightScale = transform.localScale;
        leftScale = new Vector3(-rightScale.x, rightScale.y, rightScale.z);
        
        isRunning = isFalling = isRolling = false;
        isGrounded = true;

        textToShow = "IDLE";
        playerText.text = textToShow;
        
        groundedAnimProp = Animator.StringToHash("Grounded");
        movingAnimProp = Animator.StringToHash("Moving");
        fallingAnimProp = Animator.StringToHash("Falling");
        rollAnimProp = Animator.StringToHash("Roll");
        
        meleeAttack1AnimProp = Animator.StringToHash("Melee1");
        meleeAttack2AnimProp = Animator.StringToHash("Melee2");
        meleeAttack3AnimProp = Animator.StringToHash("Melee3");

        rangeAttack1AnimProp = Animator.StringToHash("Range1");
        rangeAttack2AnimProp = Animator.StringToHash("Range2");
    }

    // Update is called once per frame
    void Update()
    {
        playerTextTransform.position = transform.position;
        
        Movement();

        HandleMeleeCombo();
        HandleRangeCombo();
    }

    private void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal > 0) transform.localScale = rightScale;
        else if (horizontal < 0) transform.localScale = leftScale;
        
        isGrounded = IsGrounded();
        
        if (vertical > 0 && isGrounded && !isRolling) myRigidBody.velocity = Vector2.up * jumpSpeed;
        myRigidBody.velocity = new Vector2(horizontal  * walkSpeed, myRigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnimator.SetTrigger(rollAnimProp);
        }

        if (isRolling) textToShow = "ROLL";
        
        if (isGrounded)
        {
            if (!isRolling)
            {
                if (Mathf.Abs(horizontal) > 0)
                {
                    isRunning = true;
                    textToShow = "RUN";
                }
                else
                {
                    isRunning = false;
                    textToShow = "IDLE";
                }
            }
        }
        else
        {
            if (myRigidBody.velocity.y > 0)
            {
                isFalling = false;
                textToShow = "JUMP";
            }
            else
            {
                isFalling = true;
                textToShow = "FALL";
            }
        }

        playerText.text = textToShow;
        myAnimator.SetBool(fallingAnimProp, isFalling);
        myAnimator.SetBool(groundedAnimProp, isGrounded);
        myAnimator.SetBool(movingAnimProp, isRunning);
    }

    private void HandleMeleeCombo()
    {
        if (Time.time - lastMeleeKeyPressed > maxComboDelay) timesMeleeKeyPressed = 0;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            lastMeleeKeyPressed = Time.time;

            timesMeleeKeyPressed++;
            
            if (timesMeleeKeyPressed == 1) myAnimator.SetBool(meleeAttack1AnimProp, true);

            timesMeleeKeyPressed = Mathf.Clamp(timesMeleeKeyPressed, 0, 3);
        }
    }

    private void HandleRangeCombo()
    {
        if (Time.time - lastRangeKeyPressed > maxComboDelay) timesRangeKeyPressed = 0;

        if (Input.GetKeyDown(KeyCode.E))
        {
            lastRangeKeyPressed = Time.time;

            timesRangeKeyPressed++;
            
            if (timesRangeKeyPressed == 1) myAnimator.SetBool(rangeAttack1AnimProp, true);

            timesRangeKeyPressed = Mathf.Clamp(timesRangeKeyPressed, 0, 2);
        }
    }

    public void ReturnMelee1()
    {
        if (timesMeleeKeyPressed >= 2) myAnimator.SetBool(meleeAttack2AnimProp, true);
        else
        {
            myAnimator.SetBool(meleeAttack1AnimProp, false);
            timesMeleeKeyPressed = 0;
        }
    }
    
    public void ReturnMelee2()
    {
        if (timesMeleeKeyPressed >= 3) myAnimator.SetBool(meleeAttack3AnimProp, true);
        else
        {
            myAnimator.SetBool(meleeAttack2AnimProp, false);
            timesMeleeKeyPressed = 0;
        }
    }
    
    public void ReturnMelee3()
    {
        myAnimator.SetBool(meleeAttack1AnimProp, false);
        myAnimator.SetBool(meleeAttack2AnimProp, false);
        myAnimator.SetBool(meleeAttack3AnimProp, false);
        timesMeleeKeyPressed = 0;
    }

    public void ReturnRange1()
    {
        if (timesRangeKeyPressed >= 2) myAnimator.SetBool(rangeAttack2AnimProp, true);
        else
        {
            myAnimator.SetBool(rangeAttack1AnimProp, false);
            timesRangeKeyPressed = 0;
        }
    }

    public void ReturnRange2()
    {
        myAnimator.SetBool(rangeAttack1AnimProp, false);
        myAnimator.SetBool(rangeAttack2AnimProp, false);
        timesRangeKeyPressed = 0;
    }

    private bool IsGrounded()
    {
        Bounds colliderBounds = myCollider.bounds;
        RaycastHit2D hit2D = Physics2D.BoxCast(colliderBounds.center, colliderBounds.size, 0f, Vector2.down, 0.1f, floorLayerMask);
        return hit2D.collider != null;
    }
}
