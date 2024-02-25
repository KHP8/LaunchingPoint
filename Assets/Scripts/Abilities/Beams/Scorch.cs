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
        thisBeam = this;
        dmg = 10;
        //maxRange = 15;
        cooldownTime = new WaitForSeconds(8);
        abilityLength = new WaitForSeconds(3);
        prefab = Resources.Load("Prefabs/Projectiles/FireBall2") as GameObject;
    }
    public override void ManageCollisionComponents(GameObject obj)
    {
        ScorchCollision scorchCollision;
        scorchCollision = obj.GetComponent<ScorchCollision>();
        scorchCollision.beam = this;
    }
}   
