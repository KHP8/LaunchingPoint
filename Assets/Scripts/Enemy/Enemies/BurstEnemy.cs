using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstEnemy : BaseEnemy
{

    private bool canBurst = true;
    private int bulletCount = 0;
    private readonly int maxBulletCount = 3;
    private readonly float burstMidpoint = .33f;
    private WaitForSeconds burstCooldown;

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
        SetBurstCooldown();  
        while (true)
        {
            if (canCast)
            {
                canCast = false;
                bulletCount = 0;
                StartCoroutine(Burst());
                StartCoroutine(ResetCastCooldown());
            }
            
            yield return null;
        }
    }

    public IEnumerator Burst()
    {
        while (bulletCount < maxBulletCount)
        {
            if (canBurst)
            {
                canBurst = false; // not putting this acts like a shotgun
                bulletCount++;
                Shot();
                StartCoroutine(ResetBurstCooldown());
            }
            yield return null;
        }
    }

    public void Shot()
    {
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

    }

    public override void StopAbility()
    {
        // Destroy the coroutine 
        if (coro != null)
            StopCoroutine(coro);
    }

    public void SetBurstCooldown()
    {
        float mod = Random.value * burstMidpoint / 20f; // random decimal nearby burstMidpoint
        mod -= Random.value * burstMidpoint / 20f; // possiblity to make it negative
        burstCooldown = new WaitForSeconds(burstMidpoint + mod);
    } 

    public IEnumerator ResetBurstCooldown()
    {
        yield return burstCooldown;
        SetBurstCooldown();
        canBurst = true;
    }

}
