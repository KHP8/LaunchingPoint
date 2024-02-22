using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public int maxRange = 25;
    public Vector3 startPoint;
    public int dmg = 10;
    public Animator animator;
    public float TimeBeforeDestroy = 2;
    public float TimeBeforeAnimation = 1;



    private void OnCollisionEnter(Collision other) {

        animator = GetComponent<Animator>();

        if (!(other.transform.CompareTag("Player")))
        {
            if (other.transform.CompareTag("Enemy"))
            {
                Debug.Log("Hit Enemy");
                other.transform.GetComponentInParent<EnemyHealth>().TakeDamage(dmg);
            }
            else
            {
                Debug.Log("Not an Enemy Hit");
            }

            // Regardless of what is hit, destroy the projectile
            Invoke("AnimationCollisionStart", TimeBeforeAnimation);
            Destroy(gameObject,TimeBeforeDestroy);
            
        }
        else if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
        }

        
    }
    void AnimationCollisionStart()
    {
        animator.SetTrigger("CollisionDetected");
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    void Update()
    {
        // If the projectile goes too far AKA off scene, destroy it
        if (Vector3.Distance(startPoint, transform.position) > maxRange)
        {
            Destroy(gameObject);
        }
    }
}
