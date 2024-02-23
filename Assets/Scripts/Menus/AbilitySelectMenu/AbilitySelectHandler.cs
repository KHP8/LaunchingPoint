using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelectHandler : MonoBehaviour
{
    public GameObject abilityButton;
    public GameObject player;

    public void OnClick()
    {
        if (abilityButton.name == "Fireball")
        {
            Fireball f = player.AddComponent<Fireball>();
            //player.GetComponent<InputManager>().player.LeftClick.performed -= ctx;
            player.GetComponent<InputManager>().player.LeftClick.performed += ctx => f.UseAbility();
        }
    }
}
