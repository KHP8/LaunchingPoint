using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour

{
    public bool useEvents;
    //message displayed to player when looking at interactable
    public string promptMessage = "Open Door";

    //called from our player
    public void BaseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
        //abstract method to be overidden
    }
}
