using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : BaseWave
{
    public override void Awake()
    {
        // BaseAbility fields
        abilityName = "FireWave";
        castSFX = Resources.Load<AudioClip>("Audio/AbilitySFX/FireWaveCast");

        // BaseProjectile fields
        dmg = 10;
        speed = 10;
        maxRange = 50;
        cooldownFloat = 8f;
        cooldown = new WaitForSeconds(cooldownFloat);
        knockbackMod = 200;
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
