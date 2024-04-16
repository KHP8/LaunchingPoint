using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownButtons : MonoBehaviour
{
    [HideInInspector] public WaitForSeconds cooldown;

    public void CooldownSelector(string abilityType)
    {
        string selectedAbility = PlayerPrefs.GetString(abilityType);
        BaseAbility abilityComponent;

        abilityComponent = GetComponentInParent<abilityComponent>()
        cooldown = abilityComponent.
    }

    private IEnumerator UpdateAbilityTimer()
    {
        for (int i = cooldown; i > 0; i--)
        {
            yield return 1;
        }
    }
}
