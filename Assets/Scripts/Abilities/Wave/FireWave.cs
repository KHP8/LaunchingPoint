using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : BaseWave
{
    public override void Awake()
    {
        // BaseAbility fields
        abilityName = "FireWave";
        //image.sprite = Resources.Load("AbilityIcons/KHP8Logo1") as Sprite;

        // BaseProjectile fields
        dmg = 10;
        rpm = 40;
        speed = 10;
        maxRange = 50;
        cooldownFloat = 8f;
        cooldown = new WaitForSeconds(cooldownFloat);
        prefab = Resources.Load("Prefabs/Projectiles/FireWave") as GameObject;
    }

    public override void ManageCollisionComponents(GameObject obj)
    {
        FirewaveCollision firewaveCollision;
        firewaveCollision = obj.GetComponent<FirewaveCollision>();
        firewaveCollision.startpoint = projectileSource.transform.position;
        firewaveCollision.projectile = this;
        firewaveCollision.player = gameObject;
    }
}
