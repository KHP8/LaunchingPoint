using UnityEngine;

public class Scorch : BaseBeam
{
    public override void Awake()
    {
        // BaseAbility fields
        abilityName = "Scorch";
        castSFX = Resources.Load<AudioClip>("Audio/AbilitySFX/ScorchCast");

        // BaseBeam fields
        dmg = 2;
        cooldownFloat = 5f;
        cooldown = new WaitForSeconds(cooldownFloat);
        abilityLength = new WaitForSeconds(3);
        prefab = Resources.Load("Prefabs/Beams/ScorchBeam") as GameObject;
    }
    public override void ManageCollisionComponents(GameObject obj)
    {
        ScorchCollision scorchCollision;
        scorchCollision = obj.GetComponentInChildren<ScorchCollision>();
        scorchCollision.beam = obj.GetComponentInChildren<Scorch>();
        scorchCollision.player = gameObject;
    }
}   
