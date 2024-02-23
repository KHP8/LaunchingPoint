using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollision : MonoBehaviour
{
    [HideInInspector] public BaseProjectile projectile;
    [HideInInspector] public Vector3 startpoint;

    private void OnCollisionEnter(Collision other) 
    {
        if (!other.transform.CompareTag("Player"))
        {
            if (other.transform.CompareTag("Enemy"))
            {
                Debug.Log("Hit Enemy");
                other.transform.GetComponentInParent<EnemyHealth>().TakeDamage(projectile.dmg);
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
        if (Vector3.Distance(startpoint, transform.position) > projectile.maxRange)
        {
            Destroy(gameObject);
        }
    }
}
