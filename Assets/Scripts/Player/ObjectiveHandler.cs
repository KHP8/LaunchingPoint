using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveHandler : MonoBehaviour
{
    public GameObject text;
    public GameObject image;

    public void SetObjectiveText(string objectiveText)
    {
        text.GetComponent<TextMeshProUGUI>().text = objectiveText;
    }

    public void CompleteObjective()
    {
        text.GetComponent<TextMeshProUGUI>().color = Color.green;
        text.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
    }
}
