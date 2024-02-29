using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBeamCollision : MonoBehaviour
{
    public BaseBeam beam;
    private bool tickDmg = true;
    private WaitForSeconds dmgTimer = new WaitForSeconds(.1f);

    private void OnTriggerStay(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
        {
            if (other.transform.CompareTag("Enemy"))
            {
                Debug.Log("Hit Enemy");
                if (tickDmg)
                {
                    other.transform.GetComponentInParent<EnemyHealth>().TakeDamage(beam.dmg);
                    tickDmg = false;
                    StartCoroutine(DamageTimer());
                }
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

    IEnumerator DamageTimer()
    {
        yield return dmgTimer;
        tickDmg = true;
    }
}
