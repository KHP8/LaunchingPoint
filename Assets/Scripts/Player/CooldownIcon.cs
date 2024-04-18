using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownIcon : MonoBehaviour
{
    public float cooldown;
    public GameObject textBox;
    public GameObject icon;
    private bool canStartTimer = true;

    public void CooldownSelector(float abilityCooldown)
    {
        cooldown = abilityCooldown;
        StartCoroutine(UpdateAbilityTimerImage());
    }

    public IEnumerator UpdateAbilityTimer()
    {
        if(canStartTimer)
        {
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

    public IEnumerator UpdateAbilityTimerImage()
    {
        float percent = 1;
        icon.GetComponent<Image>().fillAmount = percent;
        if (canStartTimer)
        {
            canStartTimer = false;
            for (float i = cooldown; i >= 0; i -= .1f)
            {
                percent -= (1 / (cooldown * 10));
                icon.GetComponent<Image>().fillAmount = percent;
                yield return new WaitForSeconds(.1f);
            }
            canStartTimer = true;
        }
    }
}
