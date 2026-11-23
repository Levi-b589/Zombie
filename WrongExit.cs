using UnityEngine;

public class WrongExitTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip wrongExitSound;

    private bool alreadyTriggered;

    //When the user walks into the staircase when they dont have the key they die or if they walk into the wrong staircase when they have the key they die
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponentInParent<PlayerHealth>();

        if (health == null || alreadyTriggered)
        {
            return;
        }

        alreadyTriggered = true;

        if (audioSource != null && wrongExitSound != null)
        {
            audioSource.PlayOneShot(wrongExitSound);
        }

        health.TakeDamage(9999);
    }
}