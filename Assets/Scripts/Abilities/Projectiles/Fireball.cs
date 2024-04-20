using UnityEngine;

public class Fireball : BaseProjectile
{
    public override void Awake()
    {
        // BaseAbility fields
        abilityName = "Fireball";
        castSFX = Resources.Load<AudioClip>("Audio/AbilitySFX/FireballCast");


        // BaseProjectile fields
        dmg = 10;
        speed = 40;
        maxRange = 50;
        cooldownFloat = 1.2f;
        cooldown = new WaitForSeconds(cooldownFloat);
        prefab = Resources.Load("Prefabs/Projectiles/FireBall") as GameObject;
    }

    public override void ManageCollisionComponents(GameObject obj)
    {
        FireballCollision fireballCollision;
        fireballCollision = obj.GetComponent<FireballCollision>();
        fireballCollision.startpoint = projectileSource.transform.position;
        fireballCollision.projectile = this;
        fireballCollision.player = gameObject;
    }
}
