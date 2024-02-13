using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCube : Interactable
{
    public GameObject particle;

    // Start is called before the first frame update
    protected override void Interact()
    {
        GameObject x = Instantiate(particle, transform.position, Quaternion.identity);
        x.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }
}
