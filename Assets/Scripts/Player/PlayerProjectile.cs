using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private int maxRange = 400;
    public Vector3 startPoint;
    public int dmg = 10;

    private void OnCollisionEnter(Collision other) {
        //Debug.Log("Hit Player");
        if (other.transform.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            other.transform.GetComponentInParent<EnemyHealth>().TakeDamage(dmg);
        }
        else
        {
            Debug.Log("Not an Enemy Hit");
        }
        Destroy(gameObject);
    }

    void Update()
    {
        if (Vector3.Distance(startPoint, transform.position) > maxRange)
        {
            Destroy(gameObject);
        }
    }
}
