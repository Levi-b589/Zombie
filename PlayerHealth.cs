using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private float timeBeforeReturningToStart = 2f;
    [SerializeField] private AudioSource hurtAudio;
    [SerializeField] private AudioClip hurtSound;

    private int currentHealth;
    private bool isDead;

    public bool IsDead => isDead;

    //Start at max health
    private void Start()
    {
        Time.timeScale = 1f;
        pickupcontroller.slotFull = false;
        KeyPickup.hasKey = false;
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }
    }

    //Taking damage and making a noise when hit
    public void TakeDamage(int damage)
    {
        if (isDead || damage <= 0)
        {
            return;
        }

        if (hurtAudio != null && hurtSound != null)
        {
            hurtAudio.PlayOneShot(hurtSound);
        }

        currentHealth = Mathf.Max(0, currentHealth - damage);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth == 0)
        {
            isDead = true;

            if (deathScreen != null)
            {
                deathScreen.SetActive(true);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // This uses real time, so it still counts while gameplay is paused.
            Time.timeScale = 0f;
            StartCoroutine(ReturnToStartScene());
        }
    }

    private IEnumerator ReturnToStartScene()
    {
        yield return new WaitForSecondsRealtime(timeBeforeReturningToStart);

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}