using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FirewaveCollision : BaseWaveCollision
{
    public void Awake()
    {
        timeBeforeDestroy = .9f;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
        {
            if (other.transform.CompareTag("Enemy"))
            {
                if (hitList.Contains(other.gameObject) == false)
                {
                    Debug.Log("Hit Enemy");
                    other.transform.GetComponentInParent<EnemyHealth>().TakeDamage(projectile.dmg, player);
                    other.transform.GetComponentInParent<BaseEnemy>().Knockback(transform.position, projectile.knockbackMod);
                    hitList.Add(other.gameObject);
                }
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
        Destroy(gameObject, timeBeforeDestroy);
    }
}
