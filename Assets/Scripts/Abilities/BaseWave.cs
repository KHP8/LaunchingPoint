using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseWave : BaseAbility
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
    public float knockbackMod;

    // Handles cooldowns and actual shooting
    //[HideInInspector] 
    public bool canShoot = true;
    public WaitForSeconds delay;
    public Coroutine coro;

    // All children should also use the Awake() method to assign values

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
                Debug.Log("CASTFIREWAVE");
                canShoot = false;
                Camera cam = GetComponent<PlayerLook>().cam;

                // Create a projectile oriented towards camera direction
                GameObject proj = Instantiate(
                    prefab,
                    projectileSource.position,
                    Quaternion.Euler(
                        0, //transform.eulerAngles.x + 90,
                        transform.eulerAngles.y,
                        0
                    )
                );

                // Give bullet physics and movement. Then manage collision script - unique data
                proj.GetComponent<Rigidbody>();
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
