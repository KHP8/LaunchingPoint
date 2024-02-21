using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileFireball : MonoBehaviour
{
    private int maxRange = 60;
    public Vector3 startPoint;
    public int dmg = 50;

    private void OnCollisionEnter(Collision other) {

        if (!(other.transform.CompareTag("Player")))
        {
            if (other.transform.CompareTag("Enemy"))
            {
                Debug.Log("Hit Enemy");
                other.transform.GetComponentInParent<EnemyHealth>().TakeDamage(dmg);
            }
            else
            {
                Debug.Log("Not an Enemy Hit");
            }

            // Regardless of what is hit, destroy the projectile
            Destroy(gameObject);
        }
        else if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
        }
    }

    void Update()
    {
        // If the projectile goes too far AKA off scene, destroy it
        if (Vector3.Distance(startPoint, transform.position) > maxRange)
        {
            Destroy(gameObject);
        }
    }
}
