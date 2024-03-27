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
        cooldown = new WaitForSeconds(2);
        abilityLength = new WaitForSeconds(1);
        prefab = Resources.Load("Prefabs/Summons/FireNuke") as GameObject;
    }
    public override void ManageCollisionComponents(GameObject obj)
    {
        FireNukeCollision fireNukeCollision;
        fireNukeCollision = obj.GetComponentInChildren<FireNukeCollision>();
        fireNukeCollision.summon = obj.GetComponentInChildren<FireNuke>();
    }
}   

