using UnityEngine;
using UnityEngine.UI;
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
    public Text[] canvasUpdate;
    PlayerInput input;

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed; 

    public bool homeReached;

    public bool isPaused = false;
    public Button pauseButton;

    public GameManager gm; 

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
        homeReached = false; 

        gm.Start();
       

    }

   
    void Update()
    {
        handleMovement();
        handleRotation();
    }

    void handleRotation()
    {
        Vector3 currentPosition = transform.position; 
        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);
        Vector3 positionToLookAt = currentPosition + newPosition;

        transform.LookAt(positionToLookAt);
    }



    void OnApplicationPause(bool pauseStatus) {

		if (gm!=null) {
			// if Game is paused, savegame
			if (pauseStatus) {
				// Save Game data
				//gm.SaveGameStatus ();
			} else {
				// Load Game data
				gm.LoadGameStatus ();
			}
		}

		
	}

    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "home")
        {
            col.gameObject.GetComponent<Renderer>().material.color = Color.green;
            canvasUpdate[0].text = "Home!";
            homeReached = true; 

        }
     
    }
    
    void OnTriggerEnter (Collider col)
	{
		// The collision will return the gameObject itself- the name property allows different
		// hitting a Coin benefits the economy!
		if (col.gameObject.name == "Coin") {
			// Destroy Coin
			Destroy (col.gameObject);
			//now update the state data
			gm.gameStatus.coinsCollected += 1;
		}//end of collision condition
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
