using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public Transform lookAtObj;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input) 
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //calculate camera rotation for looking up and down
        xRotation -= mouseY * Time.deltaTime * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        //apply this to our look at transform
        lookAtObj.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //rotate player to look left and right
        transform.Rotate(mouseX * Time.deltaTime * xSensitivity * Vector3.up);
    }
}
