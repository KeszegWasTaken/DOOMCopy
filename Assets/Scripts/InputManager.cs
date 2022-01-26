using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerActions playerActionsScript;
    [SerializeField] MouseLook mouseLookScript;
    PlayerControls controls;
    PlayerControls.PlayerActions playerActions;
    Vector2 movementInput;
    Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerControls();
        playerActions = controls.Player;

        //wasd
        playerActions.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        //space
        playerActions.Jump.performed += _ => playerActionsScript.JumpInput();
        
        //mousemovement
        playerActions.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        playerActions.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
        //shift
        playerActions.Dash.performed += _ => playerActionsScript.DashInput();

        
        
    }

    private void Update()
    {
        playerActionsScript.GetInput(movementInput);
        mouseLookScript.GetInput(mouseInput);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    
}
