using UnityEngine;

public class Scorch : BaseBeam
{
    public override void Awake()
    {
        // BaseAbility fields
        abilityName = "Scorch";
        //image.sprite = Resources.Load("AbilityIcons/KHP8Logo1") as Sprite;

        // BaseBeam fields
        dmg = 1;
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
    }
}   
