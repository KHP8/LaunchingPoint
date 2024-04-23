using System.Collections;
using UnityEngine;

/*
    This script acts as the parent to all beam scripts. 
    Those scripts should be attached to the player from the AbilitySelect menu.

    The AbilitySelect menu will handle assigning the UseAbility and StopAbilty methods 
    to the InputManager (and will assign them properly to primary, secondary, etc).

    The prefab, delay, and all weapon stats should be assigned on the child script.

    All other variables are assigned inside of functions.

    All children should use the Awake function to assign values.
*/

abstract public class BaseBeam : BaseAbility
{
    // References to locations
    [HideInInspector] public Vector3 beamSource;
    [HideInInspector] public GameObject prefab;
    [HideInInspector] public GameObject thisBeam;
    [HideInInspector] public Transform parent;

    [Header("Weapon Stats")]
    public float dmg; // Per 1/10 second

    public WaitForSeconds abilityLength;


    public override bool UseAbility()
    {
        return ShootSpell();
    }

    /// <summary>
    /// Internal which handles creating and managing beams
    /// </summary>
    private bool ShootSpell()
    {
        if (canCast) // If ability is not on cooldown
        {
            Debug.Log("CASTBEAM");
            canCast = false;

            // Create a projectile oriented towards camera direction
            GameObject beam = Instantiate(
                prefab,
                beamSource,
                Quaternion.Euler(
                    0,
                    transform.eulerAngles.y,
                    0
                ),
                parent
            );

            thisBeam = beam;

            ManageCollisionComponents(beam);

            // Play cast SFX
            GameObject.Find("AbilityCastSFXList").GetComponent<AbilityCastSFX>().PlayAbilityAudio(castSFX);

            // Begin ability length timer
            StartCoroutine(DeleteAbiltiyAfterTime());

            // Begin cooldown between ability uses
            StartCoroutine(ResetCastCooldown());
            return true;
        }

        return false;
    }

    public override void StopAbility()
    {
        
    }

    /// <summary>
    /// Destroys the beam after the beam's duration
    /// </summary>
    /// <returns></returns>
    IEnumerator DeleteAbiltiyAfterTime()
    {
        yield return abilityLength;
        Destroy(thisBeam);
    }

}
