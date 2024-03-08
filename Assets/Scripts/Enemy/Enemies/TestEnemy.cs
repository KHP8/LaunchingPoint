using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : BaseEnemy
{
    public override void Awake()
    {
        
    }

    public override void ManageCollisionComponents(GameObject obj)
    {
        
    }

    public override IEnumerator Shoot()
    {
        // Creates a projectile 
        while (true)
        {
            if (canCast) // If timer is done
            {
                //store a reference to the gun barrel
                Transform gunBarrel = projectileSource;
                //instantiate a new bullet
                GameObject bullet = Instantiate(prefab, gunBarrel.position, transform.rotation);
                //calculate direction to player
                Vector3 shootDirection = (target.transform.position - gunBarrel.transform.position).normalized;
                //add force rigidbody of the bullet
                bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-.1f, .1f) , Vector3.up) * shootDirection * 40; // 40 = bullet speed

                Debug.Log("Shooting");

                ManageCollisionComponents(bullet);

                StartCoroutine(ResetCastCooldown());
            }
            yield return null;
        }
    }

    public override void StopAbility()
    {
        
    }

}
