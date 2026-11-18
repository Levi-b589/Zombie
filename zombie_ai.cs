using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class zombie_ai : MonoBehaviour
{
//var for zombie 
    public int health = 100;
    public Animator zombieAnimator;
    public Transform player;
    public float detectionRange = 10f;
    public float attackDistance = 3f;
    public float attackInterval = 2f;

//this is for navigatetion, animation, checking to see if it is dead or not 
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
//this is for tracking the player
        float distance = Vector3.Distance(transform.position, player.position);
//this is saying if the distance is less then the dectionrange it should start moving 
        if (distance <= detectionRange)
        {
//starts playing a walking animation once in range and sets it destination to the player.
            zombieAnimator.Play("Zombie_Walk");
            Agent.SetDestination(player.position);
           anim.SetBool("isWalking", true);

           //another if statement in this if statement for attack 
           //checking to see if the zombie is in the correct distance and is attacking.
            if (distance <= attackDistance && isAttacking)
            {
            //this code might be wrong I will look over it again 
                StartCoroutine(PlayHitAnimation());
            }
        }

        //this is when the player is outside of the zombie range 
        else
        {
            Agent.ResetPath();
            anim.SetBool("isWalking", false);
            // when zombie looses the player
        }
    }
//this is when the zombie is being hit and if the zombie goes to zero it dies 
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
    //plays a hit animation when hit 
    IEnumerator PlayHitAnimation()
    {
        anim.SetTrigger("Hit");
        yield return null;
    }
    //plays attack animation 
    IEnumerable PlayerAttackAnimation()
    {
        isAttacking = true;
        Agent.isStopped = true;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(attackInterval);

        Agent.isStopped = false;
        isAttacking = false;
    }
//this is for it's death 
    void Die()
    {
        isDead = true;
        Agent.isStopped = true;
        
        // sets animation to is walking to false and triggers a death animation
        anim.SetBool("isWalking", false);
        anim.SetTrigger("dead");
    }
}
