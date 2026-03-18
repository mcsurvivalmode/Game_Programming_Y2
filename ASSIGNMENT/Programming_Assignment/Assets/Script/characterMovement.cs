using UnityEngine;
using UnityEngine.UI;
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

    bool homeReached;
    int coinsCollected;
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
        handleRotation();
    }

    void handleRotation()
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
            coinsCollected += 1;
            //Debug.Log(coinsCollected);
        }
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
