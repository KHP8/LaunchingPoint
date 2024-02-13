using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private int maxRange = 400;
    public Vector3 startPoint;
    public int dmg = 10;

    private void OnCollisionEnter(Collision other) {
        Debug.Log("Hit Player");
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            other.transform.GetComponent<PlayerHealth>().TakeDamage(dmg);
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
