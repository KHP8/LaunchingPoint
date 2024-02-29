using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBeamCollision : MonoBehaviour
{
    [HideInInspector] public BaseBeam beam;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
        {
            if (other.transform.CompareTag("Enemy"))
            {
                Debug.Log("Hit Enemy");
                other.transform.GetComponentInParent<EnemyHealth>().TakeDamage(beam.dmg);
            }
            else
            {
                Debug.Log("Not an Enemy Hit");
            }
        }
        else if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
        }
    }
}
