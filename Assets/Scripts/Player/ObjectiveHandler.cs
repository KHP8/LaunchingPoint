using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectiveHandler : MonoBehaviour
{
    public GameObject text;
    public GameObject image;

    public TMP_StyleSheet style;

    public void Awake()
    {
        style = (TMP_StyleSheet)AssetDatabase.LoadAssetAtPath("Assets/TextMesh Pro/Resources/Style Sheets/ObjectiveTextStyles.asset", typeof(TMP_StyleSheet));
        text.GetComponent<TextMeshProUGUI>().styleSheet = style;
    }

    public void SetObjectiveText(string objectiveText)
    {
        text.GetComponent<TextMeshProUGUI>().text = objectiveText;
    }

    public void SetObjectiveStyle(string styleType)
    {
        text.GetComponent<TextMeshProUGUI>().textStyle = style.GetStyle(styleType);
    }
}
