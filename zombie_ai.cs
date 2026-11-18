using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class zombie_ai : MonoBehaviour
{
    public int health = 100;
    public Animator zombieAnimator;
    public Transform player;
    public float detectionRange = 10f;
    public float attackDistance = 3f;
    public float attackInterval = 2f;

    NavMeshAgent Agent;
    Animator anim;
    bool isDead = false;
    bool isAttacking = false;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    private void Update()
    {
        
        if (isDead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {

            zombieAnimator.Play("Zombie_Walk");
            Agent.SetDestination(player.position);
           anim.SetBool("isWalking", true);
            if (distance <= attackDistance && isAttacking)
            {
                StartCoroutine(PlayHitAnimation());
            }
        }
        else
        {
            Agent.ResetPath();
            anim.SetBool("isWalking", false);
            // when zombie looses the player
        }
    }

    public void Hit(int damage)
    {
        if (isDead) return;
        health -= damage;

            if(health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(PlayHitAnimation());
        }
    }
    IEnumerator PlayHitAnimation()
    {
        anim.SetTrigger("Hit");
        yield return null;
    }
    IEnumerable PlayerAttackAnimation()
    {
        isAttacking = true;
        Agent.isStopped = true;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(attackInterval);

        Agent.isStopped = false;
        isAttacking = false;
    }

    void Die()
    {
        isDead = true;
        Agent.isStopped = true;
        anim.SetBool("isWalking", false);
        anim.SetTrigger("dead");
    }
}
