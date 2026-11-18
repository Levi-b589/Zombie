using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GamePlayMenuScript : MonoBehaviour
{
    // Panels
    public GameObject pauseCanvas;
    public GameObject optionsPanel;
    public GameObject pausePanel;

    // Camera
    public Camera playerCamera;

    // Sliders
    public Slider fovSlider;
    public Slider sensitivitySlider;
    public Slider volumeSlider;

    // Settings
    public float mouseSensitivity;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        Time.timeScale = 1f;

        // Start gameplay with the cursor locked and hidden.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Load saved settings.
        float savedFOV = PlayerPrefs.GetFloat("FOV", 90f);
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 0.1f);
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);

        if (fovSlider != null)
            fovSlider.value = savedFOV;

        if (sensitivitySlider != null)
            sensitivitySlider.value = savedSensitivity;

        if (volumeSlider != null)
            volumeSlider.value = savedVolume;

        ChangeFOV(savedFOV);
        ChangeSensitivity(savedSensitivity);
        ChangeVolume(savedVolume);
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (optionsPanel != null && optionsPanel.activeSelf)
            {
                CloseOptions();
            }
            else if (pausePanel != null && pausePanel.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        // Keep pauseCanvas active so this script can detect Escape.

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenOptions()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(true);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (optionsPanel != null)
            optionsPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeFOV(float value)
    {
        if (playerCamera != null)
            playerCamera.fieldOfView = value;

        PlayerPrefs.SetFloat("FOV", value);
    }

    public void ChangeSensitivity(float value)
    {
        mouseSensitivity = value;
        PlayerPrefs.SetFloat("Sensitivity", value);
    }

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }
}