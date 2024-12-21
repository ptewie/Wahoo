using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mainAudioMixer;

    [Header("Volume Sliders")]
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;
    
    [Header("Resolution Options")]
    [SerializeField] private List<String> resolutionOptions;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle isFullscreenToggle;


    private void Awake()
    {
        SetResolutionOptions();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetResolutionOptions()
    {
        // Make a new list of the text we want to show as resolution options. 
        // This list is parallel to our Screen.resolutions array, so the index in one array lines up with the same index in the other array!
        resolutionOptions = new List<string>();
        for (int i = 0; i<Screen.resolutions.Length; i++)
        {
            resolutionOptions.Add(Screen.resolutions[i].width + "x" + Screen.resolutions[i].height + " :" + Screen.resolutions[i].refreshRateRatio);            
        }
        // Clear the dropdown
        resolutionDropdown.ClearOptions();

        // Add those options to the dropdown
        resolutionDropdown.AddOptions(resolutionOptions);
    }

    public void SetResolution()
    {
        Screen.SetResolution(Screen.resolutions[resolutionDropdown.value].width, Screen.resolutions[resolutionDropdown.value].height, isFullscreenToggle.isOn);
    }
}
