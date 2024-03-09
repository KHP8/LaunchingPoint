using UnityEngine;

public class TestEnemyCollision : BaseEnemyCollision
{

    public override void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Enemy"))
        {
            if (other.transform.CompareTag("Player"))
            {
                Debug.Log("Hit Player");
                other.transform.parent.GetComponentInParent<PlayerHealth>().TakeDamage(baseEnemy.dmg);
            }
            else
            {
                Debug.Log("Not a Player Hit");
            }

            //AnimationCollisionStart();

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

    }

    public override void Update()
    {
        // If the projectile goes too far AKA off scene, destroy it
        if (Vector3.Distance(startpoint, transform.position) > baseEnemy.maxRange)
        {
            Destroy(gameObject);
        }
    }
}
