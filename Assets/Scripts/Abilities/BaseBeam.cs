using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

abstract public class BaseBeam : BaseAbility
{
    // References to locations
    public Transform beamSource;
    public GameObject prefab;
    public GameObject thisBeam;
    public Transform parent;

    [Header("Weapon Stats")]
    public float dmg;
    public float maxRange;

    public bool canBeam = true;
    public WaitForSeconds cooldownTime;
    public WaitForSeconds abilityLength;

    abstract public void ManageCollisionComponents(GameObject obj);

    public override void UseAbility()
    {
        ShootSpell();
    }

    private void ShootSpell()
    {
        if (canBeam) // If ability is not on cooldown
        {
            Debug.Log("CASTBEAM");
            canBeam = false;
            Camera cam = GetComponent<PlayerLook>().cam;

            Vector3 tempScorchPosition = new Vector3(
                parent.transform.position.x + 2.5f,
                parent.transform.position.y + 1.65f,
                parent.transform.position.z + 5f);

            // Create a projectile oriented towards camera direction
            GameObject beam = Instantiate(
                prefab,
                tempScorchPosition,
                Quaternion.Euler(
                    90,
                    parent.transform.rotation.y + 180,
                    0
                ),
                parent
            );

            thisBeam = beam;

            ManageCollisionComponents(beam);

            // Begin ability length timer
            StartCoroutine(DeleteAbiltiyAfterTime());

            // Begin cooldown between ability uses
            StartCoroutine(ResetCastCooldown());
        }
    }

    public override void StopAbility()
    {
        ;
    }

    IEnumerator DeleteAbiltiyAfterTime()
    {
        yield return abilityLength;
        Destroy(thisBeam);
    }

    IEnumerator ResetCastCooldown()
    {
        yield return cooldownTime;
        canBeam = true;
    }
}
