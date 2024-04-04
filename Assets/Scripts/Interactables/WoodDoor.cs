using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDoor : Interactable
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        promptMessage = "Open Door";
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("StayOpen"))
        {
            Destroy(animator);
            Destroy(this);
        }
    }

    public override void Interact() 
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            animator.ResetTrigger("OpenDoor");
            animator.SetTrigger("OpenDoor");
            Destroy(GetComponent<Collider>());
        }
    }
}
