using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitStairCase : MonoBehaviour
{
    [SerializeField] private GameObject winnerScreen;
    [SerializeField] private float returnDelay = 2f;

    private bool isExiting;
    //If player doesnt have key then the player cant leave
    private void Start()
    {
        KeyPickup.hasKey = false;

        if (winnerScreen != null)
        {
            winnerScreen.SetActive(false);
        }
            
    }
    //Siplays win screen
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !KeyPickup.hasKey || isExiting)
        {
            return;
        }

        isExiting = true;

        if (winnerScreen != null)
        {
            winnerScreen.SetActive(true);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(ReturnToStartScene());
    }
    //Automatically returns to start scene, IEnumerator allows it to be paused and continue later
    private IEnumerator ReturnToStartScene()
    {
        yield return new WaitForSecondsRealtime(returnDelay);

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
