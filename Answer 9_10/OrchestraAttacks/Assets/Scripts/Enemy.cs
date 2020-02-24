using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Type")]
    public string enemyType;

    [Header("Enemy Attacks")]
    public EnemyAttack[] enemyAttacks;

    [Header("Enemy Variables")]
    public KeyCode keyToAttack;
    public bool canAttack;
    public bool isAttacking;
    public float currentAttackTime;
    
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToAttack))
        {
            Attack();
        }
    }

    public void Attack()
    {
        Debug.Log("El enemigo " + gameObject.name + " ha intentado atacar");
        if (canAttack)
        {
            OrchestraSystem.instance.NotifyAttackToEnemies(this, false);
            isAttacking = true;
            Debug.Log("El sistema ha permitido el ataque a " + gameObject.name);
            StartCoroutine(WaitUntilFinishAttack());
        }
        else if (isAttacking) Debug.Log("El sistema ha bloqueado el ataque a " + gameObject.name + " debido a que se encuentra atacando");
        else Debug.Log("El sistema ha bloqueado el ataque a " + gameObject.name + " debido al bloqueo por tipo");
    }
    
    public void GetEnemyAttacks()
    {
        
    }
    
    IEnumerator WaitUntilFinishAttack()
    {
        currentAttackTime = 0f;
        isAttacking = true;
        
        while (currentAttackTime <= enemyAttacks[0].attackDuration)
        {
            currentAttackTime += Time.deltaTime;
            yield return null;
        }

        isAttacking = false;
        Debug.Log("La ventana de bloqueo de ataque generada por " + gameObject.name + " ha terminado");
        OrchestraSystem.instance.NotifyAttackToEnemies(this, true);
    }
}
