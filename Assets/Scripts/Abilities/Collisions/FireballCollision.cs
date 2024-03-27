using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/

public class FireballCollision : BaseProjectileCollision
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
                other.transform.GetComponent<EnemyHealth>().TakeDamage(projectile.dmg, player);
                other.transform.GetComponent<BaseEnemy>().Knockback(transform.position, projectile.knockbackMod);
            }
            else
            {
                Debug.Log("Not an Enemy Hit");
            }

            AnimationCollisionStart();
            // Regardless of what is hit, destroy the projectile
            Destroy(gameObject, timeBeforeDestroy);
        }
        else if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
        }
    }

    public override void AnimationCollisionStart()
    {
        
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        animator.SetTrigger("CollisionDetected");
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
