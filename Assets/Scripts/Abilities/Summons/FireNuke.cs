using UnityEngine;

public class FireNuke : BaseNuke
{
    public override void Awake()
    {
        // BaseAbility fields
        abilityName = "FireNuke";
        //image.sprite = Resources.Load("AbilityIcons/KHP8Logo1") as Sprite;

        // BaseBeam fields
        dmg = 1;
        maxRange = 20;
        cooldownFloat = 2f;
        cooldown = new WaitForSeconds(cooldownFloat);
        abilityLength = new WaitForSeconds(1);
        prefab = Resources.Load("Prefabs/Summons/FireNukeReference") as GameObject;
    }
    public override void ManageCollisionComponents(GameObject obj)
    {
        FireNukeCollision fireNukeCollision;
        fireNukeCollision = obj.GetComponentInChildren<FireNukeCollision>();
        fireNukeCollision.summon = this;
        fireNukeCollision.player = gameObject;
    }
}   

