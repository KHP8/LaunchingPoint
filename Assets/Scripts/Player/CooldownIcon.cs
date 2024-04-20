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

    // public IEnumerator UpdateAbilityTimerImageDown()
    // {
    //     float percent = 1;
    //     EmptyAbilityIcon();
    //     runes.GetComponent<Image>().fillAmount = percent;
    //     if (canStartTimer)
    //     {
    //         canStartTimer = false;
    //         for (float i = cooldown; i >= 0; i -= .1f)
    //         {
    //             percent -= (1 / (cooldown * 10));
    //             runes.GetComponent<Image>().fillAmount = percent;
    //             if(percent != 0)
    //                 yield return new WaitForSeconds(.1f);
    //         }
    //         //FillAbilityIcon();
    //         canStartTimer = true;
    //     }
    // }

    public IEnumerator UpdateAbilityTimerImageUp()
    {
        float percent = 0;
        EmptyAbilityIcon();
        runes.GetComponent<Image>().fillAmount = percent;
        if (canStartTimer)
        {
            canStartTimer = false;
            for (float i = 0; i < cooldown-0.1f; i += .1f)
            {   
                yield return new WaitForSeconds(.1f);
                percent += (1 / ((cooldown-.1f) * 10));
                runes.GetComponent<Image>().fillAmount = percent;
                if(i >= (cooldown - 0.3f) && i <= (cooldown - 0.2f))
                    StartCoroutine(AnimatedIconFill());
            }
            //FillAbilityIcon();
            canStartTimer = true;
        }
    }

    // public void FillAbilityIcon()
    // {
    //     icon.GetComponent<Image>().fillAmount = 1;
    // }

    public void EmptyAbilityIcon()
    {
        icon.GetComponent<Image>().color = new Color(255, 255, 255, 0);
    }

    public IEnumerator AnimatedIconFill()  
    {
        float percent = 0;
        for(float i = 0; i < 0.1f; i += 0.01f)
        {
            percent += (1f/10f);
            icon.GetComponent<Image>().color = new Color(255, 255, 255, percent);
            yield return new WaitForSeconds(.01f);
        }
    }
}
