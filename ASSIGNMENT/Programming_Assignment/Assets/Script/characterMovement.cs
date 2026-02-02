using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System.Diagnostics;




public class characterMovement : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    PlayerInput input;

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed; 

    void Awake()
    {
        input = new PlayerInput();

        input.characterController.Movement.performed += ctx => {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;};


        input.characterController.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
    }



    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

   
    void Update()
    {
        handleMovement();
    }

    void handleMovement()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);

        //if walking is true and false 
        if (movementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (!movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        //if walking and running is true and false 
        if ((movementPressed && runPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }

        if ((!movementPressed || !runPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }


    }

    void OnEnable()
    {
        
        input.characterController.Enable();
    }

    void OnDisable()
    {
        input.characterController.Disable();
    }

}
