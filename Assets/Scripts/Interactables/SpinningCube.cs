using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningCube : Interactable
{
    private Animator animator;
    private string AnimatingMessage = "Animating...";
    private string startMessage;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startMessage = promptMessage;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            promptMessage = startMessage;
        } 
        else {
            promptMessage = AnimatingMessage;
        }
    }

    protected override void Interact() 
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            animator.ResetTrigger("DoSpin");
            animator.SetTrigger("DoSpin");
        }
    }
}
