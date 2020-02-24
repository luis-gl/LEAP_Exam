using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrchestraSystem : MonoBehaviour
{
    public static OrchestraSystem instance;
    
    [Header("Active Enemies")]
    public List<Enemy> activeEnemies;

    [Header("System Variables")]
    public int neededAttacks;
    public int currentAttacks;
    public int randomNumber;
    public float waitTime;
    private float currentTime;
    private bool canOrchestra;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        canOrchestra = true;
    }

    public void NotifyAttackToEnemies(Enemy e, bool canAttack)
    {
        foreach (var enemy in activeEnemies)
        {
            if (enemy.enemyType.Equals(e.enemyType)) enemy.canAttack = canAttack;
        }
        
        CheckRandomNumber(e);
    }

    private void CheckRandomNumber(Enemy e)
    {
        randomNumber = Random.Range(0, 50);
        if (randomNumber >= 10 && randomNumber < 25 && currentAttacks >= neededAttacks && canOrchestra)
        {
            currentAttacks = 0;
            NotifyRandomToEnemies(e);
        }
    }

    public void AddAttack()
    {
        currentAttacks++;
        if (currentAttacks >= neededAttacks) Debug.Log("El número mínimo de ataques simples para desbloquear un ataque orquesta ha sido alcanzado");
    }

    private void NotifyRandomToEnemies(Enemy e)
    {
        bool finishSearch = false;
        foreach (var enemy in activeEnemies)
        {
            if (!enemy.enemyType.Equals(e.enemyType))
            {
                foreach (var attack in enemy.GetEnemyAttacks())
                {
                    foreach (var reaction in attack.reactionTags)
                    {
                        foreach (var otherAttack in e.GetEnemyAttacks())
                        {
                            foreach (var attackTag in otherAttack.attackTags)
                            {
                                if (reaction.Equals(attackTag))
                                {
                                    enemy.Attack();
                                    StartCoroutine(WaitForOrchestraTime());
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

    IEnumerator WaitForOrchestraTime()
    {
        currentTime = 0f;
        canOrchestra = false;

        while (currentTime <= waitTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("La ventana de tiempo de bloqueo de ataque en orquesta ha terminado");
        canOrchestra = true;
    }
}
