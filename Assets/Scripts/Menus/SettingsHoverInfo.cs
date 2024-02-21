using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsHoverInfo : MonoBehaviour
{
    public GameObject setting;
    public GameObject description;
    public Canvas UI;

    private Camera cameraToUse;

    private void Start()
    {
        description.SetActive(false);
    }

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

        bool isIntersectingSettingBox = TMP_TextUtilities.IsIntersectingRectTransform(setting.GetComponent<RectTransform>(), mousePosition, cameraToUse);

        if (isIntersectingSettingBox)
        {
            description.SetActive(true);
            return;
        }

        description.SetActive(false);
    }

}
