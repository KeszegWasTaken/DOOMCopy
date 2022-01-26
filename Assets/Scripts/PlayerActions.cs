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
    [SerializeField] float dashSpeed = 50f;
    [SerializeField] float dashTime = 0.2f;
    float dashCooldown = 2f;
    float lastDash = 0f;

    public GameObject gc;
    RuneCheck rc;
    private void Start()
    {
        rc = gc.GetComponent<RuneCheck>();
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
                if(rc.hasRune1 == false){
                    rc.hasRune1 = true;
                }
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
        float xDir = movementInput.x;
        float yDir = movementInput.y;

        if(xDir == 0 && yDir == 0){
            yDir = 1.0f;
        }

        if(rc.hasRune1 == true){
            Vector3 movementDirection = transform.right * xDir +
            transform.forward * yDir;
            while (Time.time < start + dashTime*1.5f) {
            verticalVelocity.y = 0f;
            controller.Move(movementDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
        } else{
            while (Time.time < start + dashTime) {
            verticalVelocity.y = 0f;
            Vector3 movementDirection = transform.right * xDir +
            transform.forward * yDir;
            controller.Move(movementDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
        }
        verticalVelocity.y += gravity * Time.deltaTime;
    }
}
