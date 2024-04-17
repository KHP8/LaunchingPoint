using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/



public class FireballCollision : BaseProjectileCollision
{
    public GameObject prefab2;
   
    
    public override void OnCollisionEnter(Collision other)
    {
        
        if (!other.transform.CompareTag("Player"))
        {
            if (other.transform.CompareTag("Enemy"))
            {
                Debug.Log("Hit Enemy");
                other.transform.GetComponentInParent<EnemyHealth>().TakeDamage(projectile.dmg, player);
            }
            else
            {
                Debug.Log("Not an Enemy Hit");
            }

            // Regardless of what is hit, destroy the projectile
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            GameObject explosion = Instantiate(prefab2,transform.position,transform.rotation);
            explosion.GetComponent<FireballExplosionCollision>().player = player;
            Destroy(gameObject);
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
