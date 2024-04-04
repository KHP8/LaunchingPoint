using System.Collections;
using UnityEngine;

/*
    This script acts as the parent to all beam collision scripts.
    Those scripts should be pre-attached to the prefabs.

    beam will be assigned from whatever script creates the prefab.

    OnTriggerStay handles when the prefab hits anything and deals damage when needed.
*/

public class BaseNukeCollision : MonoBehaviour
{
    [HideInInspector] public GameObject player;

    public BaseNuke summon;
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
                    other.transform.GetComponentInParent<EnemyHealth>().TakeDamage(summon.dmg, player);
                    other.transform.GetComponentInParent<BaseEnemy>().Knockback(transform.position, summon.knockbackMod);
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
