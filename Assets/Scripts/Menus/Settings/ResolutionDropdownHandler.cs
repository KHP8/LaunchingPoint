using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionDropdownHandler : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public TMP_Text placeholderText;
    Resolution[] resolutions;

    private void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;

        int currentResolution = -1;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.width
                  && resolutions[i].height == Screen.height)
                currentResolution = i;

        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.value = currentResolution;

    }

    public void OnDropdownValueChanged(int selectedIndex)
    {
        //Resolution resolution = resolutions[selectedIndex];
        Resolution resolution = resolutions[resolutionDropdown.value];
        FullScreenMode fullScreenMode = Screen.fullScreenMode;
        Screen.SetResolution(resolution.width, resolution.height, fullScreenMode);
    }
}
