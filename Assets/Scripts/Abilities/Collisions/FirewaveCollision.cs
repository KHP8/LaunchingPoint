using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewaveCollision : BaseWaveCollision
{
    public void Awake()
    {
        timeBeforeDestroy = .5f;
    }

    public override void OnCollisionEnter(Collision other)
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

        }
        else if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
        }
    }


    public override void Update()
    {
        // If the projectile goes too far AKA off scene, destroy it
        if (Vector3.Distance(startpoint, transform.position) > projectile.maxRange)
        {
            Destroy(gameObject);
        }
    }
}
