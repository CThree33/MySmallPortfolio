using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform target;
    Animator anim;
    bool isDead = false;
    float turnSpeed = 5f;
    public float damage;
    public float damageInterval; //time between an attack and another
    public bool canAttack = true;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > 2 && !isDead)
        {
            ChasePlayer();
        }
        else if (canAttack && !PlayerStats.singleton.isDead && !isDead)
        {
            AttackPlayer();
        }
        else if (PlayerStats.singleton.isDead)
        {
            DisableEnemy();
        }
    }

    public void EnemyDeathAnim()
    {
        isDead = true;
        anim.SetTrigger("ZombieDeath");
    }

    void ChasePlayer()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        agent.updatePosition = true;
        agent.SetDestination(target.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        agent.updateRotation = false;
        anim.SetBool("ZombieIsWalking", true);
        anim.SetBool("ZombieIsAttacking", false);
    }
    public void AttackPlayer()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        agent.updateRotation = false;
        agent.updatePosition = false;
        anim.SetBool("ZombieIsWalking", false);
        anim.SetBool("ZombieIsAttacking", true);
        StartCoroutine(AttackTime());
    }
    IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(damageInterval);
       // anim.SetBool("ZombieIsAttacking", false);
        PlayerStats.singleton.TakeDamage(damage);
        yield return new WaitForSeconds(damageInterval);
        canAttack = true;
    }

    void DisableEnemy()
    {
        anim.SetTrigger("ZombieIdle");
        anim.SetBool("ZombieIsAttacking", false);
        anim.SetBool("ZombieIsWalking", false);
    }
}