using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : BaseEnemy
{

    public override void ManageCollisionComponents(GameObject obj)
    {
        BaseEnemyCollision bec = obj.GetComponent<BaseEnemyCollision>();
        bec.startpoint = projectileSource.position;
        //calculate direction to player
        Vector3 shootDirection = (target.transform.position - projectileSource.position).normalized;
        //add force rigidbody of the bullet
        obj.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-1f, 1f) , Vector3.up) * shootDirection * vel;
        bec.baseEnemy = this;
    }

    public override IEnumerator Shoot()
    {
        // Creates a projectile 
        while (true)
        {
            if (canCast) // If timer is done
            {
                canCast = false;
                //store a reference to the gun barrel
                Transform gunBarrel = projectileSource;
                //instantiate a new bullet
                GameObject bullet = Instantiate(
                    prefab, 
                    gunBarrel.position, 
                    Quaternion.Euler(
                        transform.eulerAngles.x + 90,
                        transform.eulerAngles.y,
                        0
                    )
                    //transform.rotation
                );

                ManageCollisionComponents(bullet);

                StartCoroutine(ResetCastCooldown());
            }
            yield return null;
        }
    }

    public override void StopAbility()
    {
        // Destroy the coroutine 
        if (coro != null)
            StopCoroutine(coro);
    }

}
