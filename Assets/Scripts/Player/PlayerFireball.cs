using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    // FireBall
    public Transform projectileSource;
    public GameObject fireball;
    public float fireballrpm;
    public float fireballSpeed;

    private bool canCast = true;
    private WaitForSeconds castDelay;
    private Coroutine castCoroutine;
    

    void Start()
    {
        // Define the delay between shots
        castDelay = new WaitForSeconds(60 / fireballrpm);
    }

    public void StartFiring()
    {
        // Start a coroutine which performs shooting
        castCoroutine = StartCoroutine(ShootSpell());
    }

    IEnumerator ShootSpell()
    {
        // Creates a fireball 
        while (true)
        {
            if (canCast) // If timer is done
            {
                Debug.Log("CASTFIRE");
                canCast = false;
                Camera cam = GetComponent<PlayerLook>().cam;

                // Create a ball oriented towards camera direction
                GameObject proj = Instantiate(
                    fireball,
                    projectileSource.position,
                    Quaternion.Euler(
                        cam.transform.eulerAngles.x + 90,
                        transform.eulerAngles.y,
                        0
                    )
                );

                // Give bullet physics and movement
                proj.GetComponent<Rigidbody>().velocity = cam.transform.forward * fireballSpeed;
                proj.GetComponent<PlayerProjectile>().startPoint = projectileSource.position;

                // Begin cooldown between shots
                StartCoroutine(ResetCastCooldown());
            }
            yield return null;
        }
    }

    public void StopFiring()
    {
        // Destroy the coroutine 
        if (castCoroutine != null)
            StopCoroutine(castCoroutine);
    }

    IEnumerator ResetCastCooldown()
    {
        yield return castDelay;
        canCast = true;
    }
}
