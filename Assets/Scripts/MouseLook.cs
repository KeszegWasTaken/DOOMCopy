using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    //Rotate player on the y axis when mouse moves on x
    //Rotate camera on the x axis when mouse moves on y 

    [SerializeField] float sensitivityX = 15f;
    [SerializeField] float sensitivityY = 0.2f;
    float mouseX, mouseY;

    [SerializeField] Transform playerCamera;
    [SerializeField] float xCameraClamp = 90f;
    float xRotation = 0f;

    private void Update()
    {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        //Calculate and clamp rotation value
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xCameraClamp, xCameraClamp);

        //Set camera rotation to new value
        Vector3 rotation = transform.eulerAngles;
        rotation.x = xRotation;
        playerCamera.eulerAngles = rotation;

        
    }

    public void GetInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivityX;
        mouseY = mouseInput.y * sensitivityY;
    }
}
