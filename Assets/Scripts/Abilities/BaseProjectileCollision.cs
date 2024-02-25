using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script acts as the parent to all collision scripts.
    Those scripts should be pre-attached to the prefabs.

    projectile and startpoint will both be assigned from whatever script creates the prefab.

    OnCollisionEnter handles when the prefab hits anything and deals damage when needed.
*/

public class BaseProjectileCollision : MonoBehaviour
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
