using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private Transform playerTextTransform;
    private TextMeshProUGUI playerText;

    [Header("Movement Variables")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpSpeed;
    private float horizontal;
    private float vertical;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask floorLayerMask;

    [Header("Components")]
    private Rigidbody2D myRigidBody;
    private BoxCollider2D myCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        playerText = playerTextTransform.GetComponent<TextMeshProUGUI>();

        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerTextTransform.position = transform.position;
        
        Movement();
    }

    private void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        if (vertical > 0 && IsGrounded()) myRigidBody.velocity = Vector2.up * jumpSpeed;
        myRigidBody.velocity = new Vector2(horizontal  * walkSpeed, myRigidBody.velocity.y);
    }

    private bool IsGrounded()
    {
        Bounds colliderBounds = myCollider.bounds;
        RaycastHit2D hit2D = Physics2D.BoxCast(colliderBounds.center, colliderBounds.size, 0f, Vector2.down, 0.1f, floorLayerMask);
        return hit2D.collider != null;
    }
}
