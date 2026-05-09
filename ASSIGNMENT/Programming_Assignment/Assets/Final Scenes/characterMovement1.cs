using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System.Diagnostics;
using Unity.Netcode;
using UnityEngine.Rendering.UI;
using Unity.Cinemachine;
using Unity.VisualScripting;





public class CharacterMovement1 : MonoBehaviour // change back to monobehaviour 
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    public Text[] canvasUpdate;
    PlayerInput input;
    private Camera _camera; // for netcode 

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    public bool homeReached;
    int numberCoins = 10;

    public Text gameStatusUI;
    public Text gameOverUI;


    public SO_GameManager gm;




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
        gm.Start();

        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        homeReached = false;


        UpdateSceneFromManager();


    }

   
    void Update()
    {
        HandleMovement();
        HandleRotation();

     
    }

    private void FixedUpdate()
    {
        //Debug.Log (gm.gameStatus.health);

        //check current health level to determine whether player must die!
       

        if (gm.gameStatus.coinsCollected >= numberCoins)
        {
            // Update gamneoverUI with text 
            gameOverUI.text = "You Win!";
            // Reset Gamemanager variuables
            gm.resetGame();

        }

        //gameStatusUI.text = gm.UpdateStatus();
    }



    void HandleRotation()
    {
        Vector3 currentPosition = transform.position; 
        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);
        Vector3 positionToLookAt = currentPosition + newPosition;

        transform.LookAt(positionToLookAt);
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

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Coin")
        {
      
            Destroy(col.gameObject);
            gm.gameStatus.coinsCollected += 1;
        }


 
    }

 


    void HandleMovement()
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

    
    

    void OnApplicationQuit()
    {

        // Save Scene Data to the GameManager 
        SaveFromSceneToManager();

        //Debug.Log("OnApplicationQuit Called");
    }

    // Save data from the scene to the manager
    void SaveFromSceneToManager()
    {

        // Empty the GameManager NPCBalls Array so that there is always
        // the correct number of balls after some have been destroyed
       

        // Update Player Position in the GameManager with the position of the Player in the scene
        // This will be stored on the JSON file when the application quits 
        gm.gameStatus.playerPosition = GameObject.Find("Player").transform.position;

    }





    void UpdateSceneFromManager()
    {
        GameObject.Find("Player").transform.position = gm.gameStatus.playerPosition;
    }

}
