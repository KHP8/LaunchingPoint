using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script acts as the parent to all projectile scripts. 
    Those scripts should be attached to the player from the AbilitySelect menu.

    The AbilitySelect menu will handle assigning the UseAbility and StopAbilty methods 
    to the InputManager (and will assign them properly to primary, secondary, etc) 
    and will also assign Player to this script.

    WeaponStats should be assigned on the Prefab itself.

    The prefab, delay, and all weapon stats should be assigned on the child script.

    All other variables are assigned inside of functions.
*/

abstract public class BaseProjectile : BaseAbility
{
    // References to locations
    //[HideInInspector] public GameObject player; 
    public Transform projectileSource;
    public GameObject prefab;
    
    // Rate of fire, speed of proj, dmg of proj, how far it can go before being destroyed
    [Header("Weapon Stats")]
    public float rpm;
    public float speed;
    public float dmg;
    public float maxRange;

    // Handles cooldowns and actual shooting
    //[HideInInspector] 
    public bool canShoot = true;
    public WaitForSeconds delay;
    public Coroutine coro; 

    // All children should also use the Awake() method to assign values
    abstract public void ManageCollisionComponents(GameObject obj);

    public override void UseAbility()
    {
        // Start a coroutine which performs shooting
        coro = StartCoroutine(ShootSpell());
    }

    IEnumerator ShootSpell()
    {
        // Creates a projectile 
        while (true)
        {
            if (canShoot) // If timer is done
            {
                Debug.Log("CASTFIRE");
                canShoot = false;
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

                // Give bullet physics and movement. Then manage collision script - unique data
                proj.GetComponent<Rigidbody>().velocity = cam.transform.forward * speed;   
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

    IEnumerator ResetCastCooldown()
    {
        yield return delay;
        canShoot = true;
    }
}
