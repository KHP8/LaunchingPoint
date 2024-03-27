using System.Collections;
using UnityEngine;

/*
    This script acts as the parent to all projectile scripts. 
    Those scripts should be attached to the player from the AbilitySelect menu.

    The AbilitySelect menu will handle assigning the UseAbility and StopAbilty methods 
    to the InputManager (and will assign them properly to primary, secondary, etc).

    The prefab, delay, and all weapon stats should be assigned on the child script.

    All other variables are assigned inside of functions.

    All children should use the Awake function to assign values.
*/

abstract public class BaseProjectile : BaseAbility
{
    // References to locations
    public Transform projectileSource;
    public GameObject prefab;
    
    // Rate of fire, speed of proj, dmg of proj, how far it can go before being destroyed
    [Header("Weapon Stats")]
    public float rpm;
    public float speed;
    public float dmg;
    public float maxRange;

    public Coroutine coro; 

    public override void UseAbility()
    {
        // Start a coroutine which performs shooting
        coro = StartCoroutine(ShootSpell());
    }

    /// <summary>
    /// Internal which handles creating and managing projectiles
    /// </summary>
    IEnumerator ShootSpell()
    {
        // Creates a projectile 
        while (true)
        {
            if (canCast) // If timer is done
            {
                Debug.Log("CASTFIRE");
                canCast = false;
                Camera cam = GetComponent<PlayerLook>().cam;

                // Create a projectile oriented towards camera direction
                GameObject proj = Instantiate(
                    prefab,
                    projectileSource.position,
                    Quaternion.Euler(
                        cam.transform.eulerAngles.x + 90,
                        transform.eulerAngles.y,
                        0
                    )
                );

                // Raycast to find where the reticle is aiming, then set proper velocity
                RaycastHit hitInfo;
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                Debug.DrawRay(ray.origin, ray.direction); // For debugging purposes
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
                {
                    Debug.Log(hitInfo.transform.name);
                    proj.GetComponent<Rigidbody>().velocity = (hitInfo.point - projectileSource.position).normalized * speed;
                }
                else
                {
                    proj.GetComponent<Rigidbody>().velocity = cam.transform.forward * speed;
                }

                // Give bullet physics and movement. Then manage collision script - unique data
                ManageCollisionComponents(proj);

                // Begin cooldown between shots
                StartCoroutine(ResetCastCooldown());
            }
            yield return null;
        }
    }

    public override void StopAbility()
    {
        // Destroy the coroutine 
        if (coro != null)
            StopCoroutine(coro);
    }

}
