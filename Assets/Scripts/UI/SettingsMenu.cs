using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
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
