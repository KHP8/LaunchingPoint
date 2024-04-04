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
        cooldown = new WaitForSeconds(8);
        abilityLength = new WaitForSeconds(3);
        prefab = Resources.Load("Prefabs/Beams/ScorchBeam") as GameObject;
    }
    public override void ManageCollisionComponents(GameObject obj)
    {
        ScorchCollision scorchCollision;
        scorchCollision = obj.GetComponentInChildren<ScorchCollision>();
        scorchCollision.beam = this;
        scorchCollision.player = gameObject;
    }
}   
