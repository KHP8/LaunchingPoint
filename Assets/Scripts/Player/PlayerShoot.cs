using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    // Spell 1
    public Transform projectileSource;
    public GameObject bullet;
    public float rpm;
    public float bulletSpeed;
    
    private bool canShoot = true;
    private WaitForSeconds shootDelay;
    private Coroutine shootCoroutine;


    // Spell 2
    public GameObject bullet;
    public float spell2Rpm;
    public float spell2Speed;

    private bool canCast = true;
    private WaitForSeconds castDelay;
    private Coroutine castCoroutine;

    //WORKING ON SPELL 2 RN


    void Start()
    {
        // Define the delay between shots
        shootDelay = new WaitForSeconds(60 / rpm);
    }

    public void StartFiring()
    {
        // Start a coroutine which performs shooting
        shootCoroutine = StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        // Creates a bullet 
        while (true)
        {
            if (canShoot) // If timer is done
            {
                canShoot = false;
                Camera cam = GetComponent<PlayerLook>().cam;

                // Create a bullet oriented towards camera direction
                GameObject proj = Instantiate(
                    bullet, 
                    projectileSource.position, 
                    Quaternion.Euler(
                        cam.transform.eulerAngles.x + 90,
                        transform.eulerAngles.y,
                        0
                    )
                );

                // Give bullet physics and movement
                proj.GetComponent<Rigidbody>().velocity = cam.transform.forward * bulletSpeed;
                proj.GetComponent<PlayerProjectile>().startPoint = projectileSource.position;

                // Begin cooldown between shots
                StartCoroutine(ResetShootCooldown());
            }
            yield return null;
        }
    }

    public void StopFiring()
    {
        // Destroy the coroutine 
        if (shootCoroutine != null)
            StopCoroutine(shootCoroutine);
    }

    IEnumerator ResetShootCooldown()
    {
        yield return shootDelay;
        canShoot = true;
    }


    public void CastSpell2()
    {
        // Start a coroutine which performs shooting
        shootCoroutine = StartCoroutine(Shoot());
    }


    IEnumerator Spell2()
    {
        // Creates a bullet 
        while (true)
        {
            if (canCast) // If timer is done
            {
                canCast = false;
                Camera cam = GetComponent<PlayerLook>().cam;

                // Create a bullet oriented towards camera direction
                GameObject proj = Instantiate(
                    bullet,
                    projectileSource.position,
                    Quaternion.Euler(
                        cam.transform.eulerAngles.x + 90,
                        transform.eulerAngles.y,
                        0
                    )
                );

                // Give bullet physics and movement
                proj.GetComponent<Rigidbody>().velocity = cam.transform.forward * bulletSpeed;
                proj.GetComponent<PlayerProjectile>().startPoint = projectileSource.position;

                // Begin cooldown between shots
                StartCoroutine(ResetShootCooldown());
            }
            yield return null;
        }
    }

}
