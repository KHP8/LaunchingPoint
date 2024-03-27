using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireballExplosionCollision : BaseExplosionCollision
{
    // Start is called before the first frame update
     public override void OnTriggerEnter(Collider other)
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
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(gameObject, 2f);
        }
        else if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
        }
       
    }

    public override void Update()
    {
        
    }
}
