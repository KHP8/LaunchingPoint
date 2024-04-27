using UnityEngine;

public class FirePellet : BaseProjectile
{
    public override void Awake()
    {
        // BaseAbility fields
        abilityName = "FirePellet";
        castSFX = Resources.Load<AudioClip>("Audio/AbilitySFX/FireballCast");


        // BaseProjectile fields
        dmg = 10;
        speed = 40;
        maxRange = 50;
        cooldownFloat = 1.2f;
        cooldown = new WaitForSeconds(cooldownFloat);
        prefab = Resources.Load("Prefabs/Projectiles/FirePellet") as GameObject;
    }

    public override void ManageCollisionComponents(GameObject obj)
    {
        FirePelletCollision firePelletCollision;
        firePelletCollision = obj.GetComponent<FirePelletCollision>();
        firePelletCollision.startpoint = projectileSource.transform.position;
        firePelletCollision.projectile = this;
        firePelletCollision.player = gameObject;
    }
}