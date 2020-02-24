using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrchestraSystem : MonoBehaviour
{
    public static OrchestraSystem instance;
    
    [Header("Active Enemies")]
    public List<Enemy> activeEnemies;

    [Header("System Variables")]
    public int neededAttacks;
    public int randomNumber;
    public int waitTime;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NotifyAttackToEnemies(Enemy e, bool canAttack)
    {
        foreach (var enemy in activeEnemies)
        {
            if (enemy.enemyType.Equals(e.enemyType)) enemy.canAttack = canAttack;
        }
        
        CheckRandomNumber(e);
    }

    public void CheckRandomNumber(Enemy e)
    {
        if (randomNumber >= 10 && randomNumber < 25)
        {
            NotifyRandomToEnemies(e);
        }
    }

    public void NotifyRandomToEnemies(Enemy e)
    {
        bool finishSearch = false;
        foreach (var enemy in activeEnemies)
        {
            if (!enemy.enemyType.Equals(e.enemyType))
            {
                foreach (var attack in enemy.enemyAttacks)
                {
                    foreach (var reaction in attack.reactionTags)
                    {
                        foreach (var otherAttack in e.enemyAttacks)
                        {
                            foreach (var attackTag in otherAttack.attackTags)
                            {
                                if (reaction.Equals(attackTag))
                                {
                                    enemy.Attack();
                                    Debug.Log("El sistema ha originado un ataque de orquesta entre " + enemy.name + " y " + e.name);
                                    finishSearch = true;
                                    break;
                                }
                            }

                            if (finishSearch) break;
                        }

                        if (finishSearch) break;
                    }

                    if (finishSearch) break;
                }

                if (finishSearch) break;
            }
        }
    }
}
