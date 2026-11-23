using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class GraphicsSettings : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    Resolution[] resolutions;

    void Start()
    {
        // Quality
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));
        qualityDropdown.value = PlayerPrefs.GetInt("quality", QualitySettings.GetQualityLevel());
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        // Fullscreen
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // Resolution
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        var options = new List<string>();
        int current = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                current = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = current;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }
    //Sets the quality from the list I made within unity
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("quality", index);
    }
    //makes the game go to fullscreen if you arent in unity
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    //Sets the resolution
    public void SetResolution(int index)
    {
        Resolution r = resolutions[index];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
    }
}