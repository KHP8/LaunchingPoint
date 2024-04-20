using UnityEngine;

public class FireNuke : BaseNuke
{
    public override void Awake()
    {
        // BaseAbility fields
        abilityName = "FireNuke";
        castSFX = Resources.Load<AudioClip>("Audio/AbilitySFX/FireNukeCast");

        // BaseBeam fields
        dmg = 1;
        maxRange = 80;
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

