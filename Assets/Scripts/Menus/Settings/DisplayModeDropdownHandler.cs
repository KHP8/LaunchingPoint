using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DisplayModeDropdownHandler : MonoBehaviour
{
    public TMP_Dropdown displayModeDropdown;
    public TMP_Text placeholderText;

    private void Start()
    {
        displayModeDropdown.value = -1;
        displayModeDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        OnDropdownValueChanged(displayModeDropdown.value);
        if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
            placeholderText.text = $"Windowed Fullscreen";
        else if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
            placeholderText.text = $"Fullscreen";
        else if (Screen.fullScreenMode == FullScreenMode.Windowed)
            placeholderText.text = $"Windowed";
    }

    private void OnDropdownValueChanged(int selectedIndex)
    {
        if (displayModeDropdown.value == 0)
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        else if (displayModeDropdown.value == 1)
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else if (displayModeDropdown.value == 2)
            Screen.fullScreenMode = FullScreenMode.Windowed;
    }
}