using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //Panels
    public GameObject pauseCanvas;
    public GameObject optionsPanel;
    public GameObject pausePanel;
    public GameObject startMenu;

    //Camera
    public Camera playerCamera;

    //sliders
    public Slider fovSlider;
    public Slider sensitivitySlider;
    public Slider volumeSlider;

    //Settings
    public float mouseSensitivity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (pausePanel!=null) pausePanel.SetActive(false);
        if (optionsPanel != null) pausePanel.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        //Load saved settings
        float savedFOV = PlayerPrefs.GetFloat("FOV", 90f);
        float savedSens = PlayerPrefs.GetFloat("Sensitivity", 0.1f);
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);


        fovSlider.value = savedFOV;
        sensitivitySlider.value = savedSens;
        volumeSlider.value = savedVolume;

        ChangeFOV(savedFOV);
        ChangeSensitivity(savedSens);
        ChangeVolume(savedVolume);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(optionsPanel.activeSelf)
            {
                BackToStartMenu();
            }
            else if (pausePanel.activeSelf)
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
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenOptions()
    {
        pauseCanvas.SetActive(true);
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        pauseCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ChangeFOV(float value)
    {
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

    public void OpenStartOptions()
    {
        startMenu.SetActive(false);
        pauseCanvas.SetActive(true);
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void BackToStartMenu()
    {
        optionsPanel.SetActive(false);
        startMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
