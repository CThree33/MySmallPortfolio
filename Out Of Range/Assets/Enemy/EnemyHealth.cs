using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    private int itemNum;
    private int randNum;
    public Transform enemyPosition;
    EnemyAI EnemyAi;
    public WaveManager waveManager;
    public float healthEnemy;
    private bool isDead;
    public GameObject spawn;
    public Vector3 offsetPos;
    public GameObject Coin;

    void Start()
    {
        //offsetPos = new Vector3(0, 0.3f, 0);
        EnemyAi = GetComponent<EnemyAI>();
        spawn = GameObject.Find("Spawners");
        waveManager = spawn.GetComponent<WaveManager>();
    }

    public void ReduceHealth(float reduceHealth)
    {
        if (!isDead)
        {
            healthEnemy -= reduceHealth;

            if (healthEnemy <= 0)
            {
                EnemyDead();
            }
        }
    }
    public void EnemyDead()
    {
        isDead = true;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        EnemyAi.EnemyDeathAnim();
        waveManager.zombiesLeft--;
        DropItem();
        Destroy(gameObject, 2);
    }
    public void DropItem()
    {
        Instantiate(Coin, enemyPosition.position + offsetPos, Quaternion.Euler(0, 0, 90));
    }
}