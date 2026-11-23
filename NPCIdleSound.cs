using System.Collections;
using UnityEngine;

public class NPCIdleSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] idleSounds;
    [SerializeField] private float minDelay = 6f;
    [SerializeField] private float maxDelay = 14f;

    private bool isTalking;


    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        StartCoroutine(PlayIdleSounds());
    }

    //Allows the NPC to make a sound after a certain amount of time
    private IEnumerator PlayIdleSounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            if (!isTalking && audioSource != null && idleSounds.Length > 0 && !audioSource.isPlaying)
            {
                AudioClip sound = idleSounds[Random.Range(0, idleSounds.Length)];
                audioSource.PlayOneShot(sound);
            }
        }
    }
    //Stops the sound
    public void SetTalking(bool talking)
    {
        isTalking = talking;

        if (talking && audioSource != null)
        {
            audioSource.Stop();
        }
    }
}