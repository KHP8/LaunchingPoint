using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg = 10;
    private void OnCollisionEnter(Collision other) {
        Transform HitTransform = other.transform;
        if (HitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            HitTransform.GetComponent<PlayerHealth>().TakeDamage(dmg);
        }
        Destroy(gameObject);
    }
}
