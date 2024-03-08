using UnityEngine;

public class RockThrow : BaseProjectile
{
    public override void Awake()
    {
        // BaseAbility fields
        abilityName = "RockThrow";
        //image.sprite = Resources.Load("AbilityIcons/KHP8Logo1") as Sprite;

        // BaseProjectile fields
        dmg = 10;
        rpm = 50;
        speed = 40;
        maxRange = 50;
        cooldown = new WaitForSeconds(60 / rpm);
        prefab = Resources.Load("Prefabs/Projectiles/RockThrow") as GameObject;
    }

    public override void ManageCollisionComponents(GameObject obj)
    {
        RockThrowCollision rockThrowCollision;
        rockThrowCollision = obj.GetComponent<RockThrowCollision>();
        rockThrowCollision.startpoint = projectileSource.transform.position;
        rockThrowCollision.projectile = this;
    }
}
