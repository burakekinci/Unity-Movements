using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;

    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;

    public float moveAmount;

    public bool sprint_Input;
    public bool walk_Input;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private  void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>(); 

            playerControls.PlayerActions.Run.performed += i => sprint_Input = true;
            playerControls.PlayerActions.Run.canceled += i => sprint_Input = false;

            playerControls.PlayerActions.Walk.performed += i => walk_Input = true;
            playerControls.PlayerActions.Walk.canceled += i => walk_Input = false;

        }

        playerControls.Enable();

    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandeAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleWalkingInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(horizontalInput,verticalInput,playerLocomotion.isSprinting,playerLocomotion.isWalking);

    }

    private void HandleSprintingInput()
    {
        if(sprint_Input && moveAmount>=0.5f)
        {
            playerLocomotion.isSprinting = true;    
        }else{
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleWalkingInput()
    {
        if(walk_Input && moveAmount >= 0.5f)
        {
            playerLocomotion.isWalking = true;    
        }else{
            playerLocomotion.isWalking = false;
        }
    }

}
