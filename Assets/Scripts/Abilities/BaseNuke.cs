using System.Collections;
using Unity.VisualScripting;
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

abstract public class BaseNuke : BaseAbility
{
    // References to locations
    public Transform projectileSource;
    public Vector3 beamSource;
    public GameObject prefab;
    public GameObject thisBeam;
    public Transform parent;

    [Header("Weapon Stats")]
    public float dmg; // Per 1/10 second
    public float knockbackMod;

    public float maxRange;

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
            
            Camera cam = GetComponent<PlayerLook>().cam;

            // Create a projectile oriented towards camera direction
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Enemy","Default");
            Ray summonRay = new Ray(cam.transform.position, cam.transform.forward);


            // If the player was pointing at the ground

            if(Physics.Raycast(summonRay, out hit, maxRange,~mask))
            {
                Debug.Log(hit.point);
                Debug.Log("CASTBEAM");
                canCast = false;

                GameObject beam = Instantiate(
                    prefab,
                    hit.point,
                    Quaternion.identity
                );

                beam.transform.rotation = Quaternion.FromToRotation(
                        Vector3.up, hit.normal
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
