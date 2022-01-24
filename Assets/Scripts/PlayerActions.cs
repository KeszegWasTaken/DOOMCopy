using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    //wasd movement
    Vector2 movementInput;
    [SerializeField] CharacterController controller;
    [SerializeField] float movementspeed = 10f;

    //freefall and jump
    [SerializeField] float gravity = -15f;
    [SerializeField] Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    [SerializeField] float jumpHeight = 2.5f;
    bool jump = false;
    bool canDoubleJump = false;

    //dash
    bool dash = false;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashTime = 0.25f;
    float dashCooldown = 2f;
    float lastDash = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Horizontal movement
        Vector3 horizontalVelocity = (transform.right * movementInput.x +
            transform.forward * movementInput.y) * movementspeed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        //Reset out velocity to 0 when hitting the ground
        //Stepoffset to 0 when airborne, prevents glitching
        isGrounded = Physics.CheckSphere(transform.position, 0.2f, groundMask);
        if (isGrounded)
        {
            controller.stepOffset = 0.8f;
            if (verticalVelocity.y < 0)
            {
                verticalVelocity.y = -2f;
            }
        }
        else {
            controller.stepOffset = 0f;
        }

        //Jump and doublejump
        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            jump = false;
        }

        //Gravity fall
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

        //Dashing
        
        if (dash)
        {
            StartCoroutine(Dash());
            dash = false;
            
        }
    }


    public void GetInput(Vector2 _movementInput) { 
        movementInput = _movementInput;
    }

    public void JumpInput() {
        jump = true;
    }

    public void DashInput() {
        float currentDash = Time.time;

        if ((currentDash - lastDash) > dashCooldown) {
            dash = true;
            lastDash = currentDash;
        }
    }

    IEnumerator Dash() {
        float start = Time.time;
        while (Time.time < start + dashTime) {
            verticalVelocity.y = 0f;
            Vector3 movementDirection = transform.right * movementInput.x +
            transform.forward * movementInput.y;
            controller.Move(movementDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
        verticalVelocity.y += gravity * Time.deltaTime;
    }
}
