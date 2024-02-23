using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAbility : BaseAbility
{
    public void Awake()
    {
        name = "Fireball";
        image.sprite = Resources.Load("AbilityIcons/KHP8Logo1") as Sprite;
    }
}
