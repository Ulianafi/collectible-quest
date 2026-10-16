using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public float xRotation = 0f;

    public float xSensitivity = 50f;
    public float ySensitivity = 50f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //calc rotation for lookin up and down
        xRotation -= (mouseY * Time.smoothDeltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //aply this to our camer transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  
        //rotate player to look left and r
        transform.Rotate(Vector3.up * (mouseX * Time.smoothDeltaTime) * xSensitivity);
    }
}
