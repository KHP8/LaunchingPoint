using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownIcon : MonoBehaviour
{
    public float cooldown;
    public GameObject textBox;
    public GameObject runes;
    public GameObject icon;
    private bool canStartTimer = true;

    public void CooldownSelector(float abilityCooldown)
    {
        cooldown = abilityCooldown;
        StartCoroutine(UpdateAbilityTimerImageUp());
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

    public IEnumerator UpdateAbilityTimerImageDown()
    {
        float percent = 1;
        EmptyAbilityIcon();
        icon.GetComponent<Image>().fillAmount = percent;
        if (canStartTimer)
        {
            canStartTimer = false;
            for (float i = cooldown; i >= 0; i -= .1f)
            {
                percent -= (1 / (cooldown * 10));
                runes.GetComponent<Image>().fillAmount = percent;
                yield return new WaitForSeconds(.1f);
            }
            FillAbilityIcon();
            canStartTimer = true;
        }
    }

    public IEnumerator UpdateAbilityTimerImageUp()
    {
        float percent = 0;
        EmptyAbilityIcon();
        icon.GetComponent<Image>().fillAmount = percent;
        if (canStartTimer)
        {
            canStartTimer = false;
            for (float i = 0; i < cooldown; i += .1f)
            {
                percent += (1 / (cooldown * 10));
                runes.GetComponent<Image>().fillAmount = percent;
                yield return new WaitForSeconds(.1f);
            }
            FillAbilityIcon();
            canStartTimer = true;
        }
    }

    public void FillAbilityIcon()
    {
        icon.GetComponent<Image>().fillAmount = 1;
    }

    public void EmptyAbilityIcon()
    {
        icon.GetComponent<Image>().fillAmount = 0;
    }
}
