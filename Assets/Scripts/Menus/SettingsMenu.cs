using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Dropdown displayMode;
    public Dropdown resolution;

    // Start is called before the first frame update
    public void SetResolution()
    {
        if (displayMode.value == 0)
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else if (displayMode.value == 1)
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        else if (displayMode.value == 2)
            Screen.fullScreenMode = FullScreenMode.Windowed;

    }
}
