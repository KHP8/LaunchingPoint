using UnityEngine;

public class Fireball : BaseProjectile
{
    public override void Awake()
    {
        // BaseAbility fields
        abilityName = "Fireball";
        //image.sprite = Resources.Load("AbilityIcons/KHP8Logo1") as Sprite;

        // BaseProjectile fields
        dmg = 10;
        rpm = 50;
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
