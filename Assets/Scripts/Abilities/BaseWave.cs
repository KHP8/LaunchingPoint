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

    public Coroutine coro;

    // All children should also use the Awake() method to assign values

    public override bool UseAbility()
    {
        return ShootSpell();
    }

    /// <summary>
    /// Internal which handles creating and managing projectiles
    /// </summary>
    private bool ShootSpell()
    {
        if (canCast) // If timer is done
        {
            Debug.Log("CASTFIREWAVE");
            canCast = false;
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

            // Play cast SFX
            GameObject.Find("AbilityCastSFXList").GetComponent<AbilityCastSFX>().PlayAbilityAudio(castSFX);

            // Begin cooldown between shots
            StartCoroutine(ResetCastCooldown());
            return true;
        }

        return false;
    }

    public override void StopAbility()
    {
        Debug.Log("FireWave???");
        // Destroy the coroutine 
        if (coro != null)
            StopCoroutine(coro);
    }
}
