using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;

    [SerializeField] Vector3 targetDirection;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    public float walkingSpeed = 3;
    public float runningSpeed = 7;
    public float sprintingSpeed = 10;
    public bool isSprinting;
    public bool isWalking;
    public float rotationSpeed = 15;
    
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Vector3 cameraRight = cameraObject.right;
        cameraRight.y=0;
        cameraRight = cameraRight.normalized;

        Vector3 cameraForward = cameraObject.forward;
        cameraForward.y=0;
        cameraForward = cameraForward.normalized;

        

        moveDirection = cameraRight * inputManager.horizontalInput;
        moveDirection = moveDirection + cameraForward * inputManager.verticalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        //Debug.Log(moveDirection);

        /*if(isSprinting)
            moveDirection = moveDirection * sprintingSpeed;
        else if(isWalking)
            moveDirection = moveDirection * walkingSpeed;
        else
            moveDirection = moveDirection * runningSpeed;    
        */
        Vector3 movementVelocity = moveDirection * runningSpeed;
        //Debug.Log(moveDirection);
        playerRigidbody.velocity = movementVelocity; 
    }
    public Vector3 worldPos;
    private void HandleRotation()
    {
        Vector2 mousePos = inputManager.MousePosition;
        float angle = Vector2.SignedAngle(mousePos.normalized,Vector2.up);

        //worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        //Vector2 dir = mousePos.normalized - Vector2.up;
        //Debug.Log(" angle" + mousePos);

        //create a ray from the mouse cursor on screen in the direction of the camera
        Ray camRay = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(camRay.origin,camRay.direction,Color.yellow);
            
        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;
        
        
        // Perform the raycast and if it hits something on the layerMask
        if(Physics.Raycast(camRay, out floorHit,Mathf.Infinity))
        {
            
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
 
            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);

        }

        /*
        targetDirection = Vector3.zero;
        
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation,targetRotation,rotationSpeed * Time.deltaTime);
    
        transform.rotation = playerRotation;
        */
    }
    void OnDrawGizmos() {
            Gizmos.DrawSphere(new Vector3(worldPos.x,0,worldPos.y),0.2f);    
    }
}
