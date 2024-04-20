using System.Collections;
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
        obj.GetComponent<Rigidbody>().velocity = 
            Quaternion.AngleAxis(Random.Range(-accuracyRadius, accuracyRadius) , Vector3.up) * shootDirection * vel;
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
                //instantiate a new bullet
                GameObject bullet = Instantiate(
                    prefab, 
                    projectileSource.position, 
                    Quaternion.Euler(
                        transform.eulerAngles.x + 90,
                        transform.eulerAngles.y,
                        0
                    )
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
