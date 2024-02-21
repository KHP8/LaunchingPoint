using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuHover : MonoBehaviour
{
    public GameObject button;
    public Canvas UI;

    private Camera cameraToUse;

    private void Update()
    {
        if (UI.renderMode == RenderMode.ScreenSpaceCamera)
            cameraToUse = null;
        else
            cameraToUse = UI.worldCamera;

        CheckForSettingAtMouse();
    }

    private void CheckForSettingAtMouse()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Color tempColor;

        bool isIntersectingSettingBox = TMP_TextUtilities.IsIntersectingRectTransform(button.GetComponent<RectTransform>(), mousePosition, cameraToUse);
        tempColor = button.GetComponent<Image>().color;

        if (isIntersectingSettingBox)
        {
            tempColor.a = .6875f;
            button.GetComponent<Image>().color = tempColor;
            return;
        }

        tempColor.a = 0f;
        button.GetComponent<Image>().color = tempColor;
        return;
    }
}
