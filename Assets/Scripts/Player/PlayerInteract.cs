using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo; // var to store collision info
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            //Debug.Log("1");
            if (hitInfo.collider.GetComponent<Interactable>() != null) // if object to interact with
            {
                //Debug.Log("2");
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>(); // obj to interact with
                playerUI.UpdateText(interactable.promptMessage); // update on screen text
                if (inputManager.player.Interact.triggered)
                {
                    //Debug.Log("3");
                    interactable.BaseInteract();   
                }
            }
        }

    }
}
