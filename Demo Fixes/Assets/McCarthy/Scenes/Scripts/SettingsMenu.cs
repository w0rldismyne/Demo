using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown Dropdown;

    Resolution[] resolutions;

    void Start()
    { //PopulateList(); 
    }

    void PopulateList()
    {
        resolutions = Screen.resolutions;
        Dropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        Dropdown.AddOptions(options);
        Dropdown.value = currentResolutionIndex;
        Dropdown.RefreshShownValue();
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    
    public void SetQuality (int qualityIndex)
    { QualitySettings.SetQualityLevel(qualityIndex); }

    public void SetFullscreen (bool isFullscreen)
    { Screen.fullScreen = isFullscreen; }
}
