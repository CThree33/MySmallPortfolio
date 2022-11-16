using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    
    float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        
    }

    void Update()
    {
        float mouseX = 0;
        float mouseY = 0;

        if (Mouse.current != null)
        {
            mouseX = Mouse.current.delta.ReadValue().x;
            mouseY = Mouse.current.delta.ReadValue().y;
        }

        if (Gamepad.current != null)
        {
            mouseX = Gamepad.current.rightStick.ReadValue().x;
            mouseX = Gamepad.current.leftStick.ReadValue().y;
        }

        xRotation -= mouseY * Time.deltaTime * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -50, 50);
        transform.localEulerAngles = new Vector3(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX * Time.deltaTime * mouseSensitivity);
    }
}
