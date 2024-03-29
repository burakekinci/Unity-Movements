using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting, bool isWalking)
    {
        //Animation Snapping
        float snappedHorizontal;
        float snappedVertical;

        float lastHorizontal = 0;
        float lastVertical = 0;

        #region Snapped Horizontal
        if(horizontalMovement > 0.2f && !isSprinting && horizontalMovement <  1.1f)
        {
            snappedHorizontal = 0.5f;
        }else if(horizontalMovement< -0.2f && !isSprinting && horizontalMovement > -1.1f)
        {
            snappedHorizontal = -0.5f;
        }else if(horizontalMovement == 0){
            snappedHorizontal = 0;
        }
        else{
            snappedHorizontal = 0;
        }
        #endregion
        
        #region Snapped Vertical
        if(verticalMovement > 0.2f && !isSprinting &&  verticalMovement < 1.1f)
        {
            snappedVertical = 0.5f;
        }else if(verticalMovement< -0.2f && !isSprinting &&  verticalMovement > -1.1f)
        {
            snappedVertical = -0.5f;
        }else if(verticalMovement == 0)
        {
            snappedVertical = 0;
        }
        else{
            snappedVertical = 0;
        }
        #endregion

        if(isSprinting)
        {
            snappedVertical = 2;
            snappedHorizontal = horizontalMovement;
        }

        if(isWalking)
        {
            snappedVertical = 0.5f;
            snappedHorizontal = horizontalMovement;
        }
        
    
        lastVertical = verticalMovement;
        lastHorizontal = horizontalMovement;
        animator.SetFloat(horizontal,horizontalMovement,0.15f,Time.deltaTime);
        animator.SetFloat(vertical,verticalMovement,0.15f,Time.deltaTime);
    
        
    }


}
