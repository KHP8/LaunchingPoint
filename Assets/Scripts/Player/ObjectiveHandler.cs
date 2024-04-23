using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectiveHandler : MonoBehaviour
{
    public GameObject text;
    public GameObject image;

    public TMP_StyleSheet style;

    public void LoadStyles()
    {
        style = Resources.Load<TMP_StyleSheet>("StyleSheets/ObjectiveTextStyles");
        text.GetComponent<TextMeshProUGUI>().styleSheet = style;
    }

    public void SetObjectiveText(string objectiveText)
    {
        text.GetComponent<TextMeshProUGUI>().text = objectiveText;
    }

    //public void SetObjectiveStyle(string styleType)
    //{
    //    text.GetComponent<TextMeshProUGUI>().textStyle = style.GetStyle(styleType);
    //}

    public void SetObjectiveIncomplete()
    {
        text.GetComponent<TextMeshProUGUI>().font = Resources.Load<TMP_FontAsset>("Fonts/ObjectiveTextIncomplete");
    }

    public void SetObjectiveComplete()
    {
        text.GetComponent<TextMeshProUGUI>().font = Resources.Load<TMP_FontAsset>("Fonts/ObjectiveTextComplete");
    }
}
