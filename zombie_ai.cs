using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class zombie_ai : MonoBehaviour
{
    //var for zombie 
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] nearbySounds;
    [SerializeField] private float soundRange = 8f;
    [SerializeField] private float minSoundDelay = 4f;
    [SerializeField] private float maxSoundDelay = 8f;

    private float nextSoundTime;
    public int health = 100;
    public Animator zombieAnimator;
    public Transform player;
    public float detectionRange = 10f;
    public float attackDistance = 3f;
    public float attackInterval = 2f;
    public float timeBeforeDisappearing = 2f;

//this is for navigatetion, animation, checking to see if it is dead or not 
    NavMeshAgent Agent;
    Animator anim;
    bool isDead = false;
    bool isAttacking = false;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        Agent = GetComponent<NavMeshAgent>();
        anim = zombieAnimator != null ? zombieAnimator : GetComponentInChildren<Animator>();
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        Agent.stoppingDistance = attackDistance;

    }
    private void Update()
    {

        if (isDead || player == null || Agent == null || anim == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= soundRange && Time.time >= nextSoundTime && nearbySounds.Length > 0 && audioSource != null)
        {
            AudioClip sound = nearbySounds[Random.Range(0, nearbySounds.Length)];
            audioSource.PlayOneShot(sound);
            nextSoundTime = Time.time + Random.Range(minSoundDelay, maxSoundDelay);
        }

        if (distance > detectionRange)
        {
            Agent.isStopped = true;
            Agent.ResetPath();
            anim.SetBool("isWalking", false);
            return;
        }

        Agent.SetDestination(player.position);

        if (distance <= attackDistance)
        {
            Agent.isStopped = true;
            anim.SetBool("isWalking", false);

            if (!isAttacking)
            {
                StartCoroutine(PlayerAttackAnimation());
            }
        }
        else
        {
            Agent.isStopped = false;
            anim.SetBool("isWalking", Agent.velocity.sqrMagnitude > 0.01f);
        }

        //this is when the player is outside of the zombie range 
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
    IEnumerator PlayerAttackAnimation()
    {
        isAttacking = true;
        Agent.isStopped = true;
        anim.SetTrigger("Attack");

        // Delay damage so it lands during the attack animation.
        yield return new WaitForSeconds(0.5f);

        if (!isDead && player != null &&
            Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(20);
            }
        }

        yield return new WaitForSeconds(attackInterval);

        if (!isDead)
        {
            Agent.isStopped = false;
        }

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
        Destroy(gameObject, timeBeforeDisappearing);
        foreach(Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }


    }
}
