using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorch : BaseBeam
{
    private void Awake()
    {
        // BaseAbility fields
        abilityName = "Scorch";
        //image.sprite = Resources.Load("AbilityIcons/KHP8Logo1") as Sprite;

        // BaseProjectile fields
        //thisBeam = this;
        dmg = 1;
        //maxRange = 15;
        cooldownTime = new WaitForSeconds(8);
        abilityLength = new WaitForSeconds(3);
        prefab = Resources.Load("Prefabs/Beams/ScorchBeam") as GameObject;
    }
    public override void ManageCollisionComponents(GameObject obj)
    {
        ScorchCollision scorchCollision;
        scorchCollision = obj.GetComponentInChildren<ScorchCollision>();
        scorchCollision.beam = obj.GetComponentInChildren<Scorch>();
    }
}   
