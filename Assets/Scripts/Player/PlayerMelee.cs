using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public GameObject melee;
    public bool canMelee = true;
    public float meleeCooldown = 1.0f;

    public void Melee()
    {
        if (canMelee)
        {
            canMelee = false;
            Animator anim = melee.GetComponent<Animator>();
            anim.ResetTrigger("Melee");
            anim.SetTrigger("Melee");
            StartCoroutine(ResetMeleeCooldown());
        }
    }

    IEnumerator ResetMeleeCooldown()
    {
        yield return new WaitForSeconds(meleeCooldown);
        canMelee = true;
    }
}
