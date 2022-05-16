using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;

    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector2 mousePosition;
    public float verticalInput;
    public float horizontalInput;


    public float moveAmount;

    public bool sprint_Input;
    public bool walk_Input;


    public Vector2 MousePosition
    {
        get { return mousePosition;} //- new Vector2(Screen.width/2,Screen.height/2); }
    }

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
            
            //read movement input values(WASD-LeftStick)
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>(); 

            //read rotation input values(MouseDirection-RightStick)
            playerControls.PlayerMovement.MousePosition.performed += i => mousePosition = i.ReadValue<Vector2>();
            
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
        HandleRotationInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(horizontalInput,verticalInput,playerLocomotion.isSprinting,playerLocomotion.isWalking);

    }

    private void HandleRotationInput()
    {
        
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
