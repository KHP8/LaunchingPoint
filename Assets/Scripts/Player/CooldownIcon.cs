using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CooldownIcon : MonoBehaviour
{
    public float cooldown;
    public GameObject textBox;
    private bool canStartTimer = true;

    public void CooldownSelector(float abilityCooldown)
    {
        cooldown = abilityCooldown;
        StartCoroutine(UpdateAbilityTimer());

    }

    public IEnumerator UpdateAbilityTimer()
    {
        if(canStartTimer)
        {
            Debug.Log("I MADE IT TOJASHKL:WHJRGKLJj");
            canStartTimer = false;
            for (float i = cooldown; i > 0; i -= .1f)
            {
                textBox.GetComponent<TextMeshProUGUI>().text = i.ToString("F1");
                yield return new WaitForSeconds(.1f);
            }
            textBox.GetComponent<TextMeshProUGUI>().text = "";
            canStartTimer = true;
        }
    }
}
