using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsScript : MonoBehaviour
{
    public TMP_Dropdown resDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int CurrentRes = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height + ":  " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if ((resolutions[i].width == Screen.currentResolution.width) &&
                (resolutions[i].height == Screen.currentResolution.height))
            {
                CurrentRes = i;

            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = CurrentRes;
        resDropdown.RefreshShownValue();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}