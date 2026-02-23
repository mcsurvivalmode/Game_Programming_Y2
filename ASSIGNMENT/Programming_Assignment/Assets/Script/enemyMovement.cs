using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEditor.Experimental.GraphView;

public class enemyMovement : MonoBehaviour
{
    NavMeshAgent navAgent;
    Vector3 destination;

    //canvas update on state!
    public Text[] canvasUpdate;

    public bool isVisible;
    public bool isAudible;
    public bool isClose;

    //so enemy follows the player
    public Transform targetObject;
    Animator anim; 
   
    Vector3 worldDeltaPosition;
    Vector2 groundDeltaPosition;
    Vector2 velocity = Vector2.zero;

    characterMovement stateCheck;

    

  
    int nextIndex;
    public GameObject[] waypoints; //so the enemy will patrol likek guards till player is near 

    
    public float fieldOfViewAngle = 360.0f; //what the enemy can see 
    private SphereCollider col;


    void Start()
    {
        
        anim = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navAgent.Warp(transform.position);
        navAgent.updatePosition = false;

        destination = NextWaypoint(Vector3.zero);
        col = GetComponent<SphereCollider>();

        stateCheck = FindAnyObjectByType<characterMovement>();
    }

    
    void Update() //decision tree 
    {

             //this will be a state for if the player reaches home, the enemy cannot look for them anymore
        if (stateCheck == true) 
        {
            patrolFunction();
            canvasUpdate[2].text = "Player is home and safe ";

        }

        if (targetObject)
        {
            isPlayerVisible();
            isPlayerAudible();
            isPlayerClose();




            if (isVisible && isClose)
            {
                seekFunction(); //is visible and near 
            }
            else if (isVisible && !isClose)
            {
                patrolFunction(); //is visible and not near 
            }
            else if (!isVisible && !isAudible)
            {
                IdleFunction(); //isnt visible or close 
            }
            else if (!isVisible && isAudible)
            {
                patrolFunction();
            } 
        }
        else
        {
            IdleFunction();
        }

        navAgent.SetDestination(destination);
        worldDeltaPosition = navAgent.nextPosition - transform.position;
        groundDeltaPosition.x = Vector3.Dot(transform.right, worldDeltaPosition);
        groundDeltaPosition.y = Vector3.Dot(transform.forward, worldDeltaPosition);
        velocity = (Time.deltaTime > 1e-5f) ? groundDeltaPosition / Time.deltaTime : velocity = Vector2.zero;
        bool shouldMove = velocity.magnitude > 0.025f && navAgent.remainingDistance > navAgent.radius;

        anim.SetBool("move", shouldMove);
        anim.SetFloat("velx", velocity.x);
        anim.SetFloat("vely", velocity.y);
        Debug.Log("ShouldMove: " + shouldMove);
    }


    void OnAnimatorMove()
    {
        transform.position = navAgent.nextPosition;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "player")
        {
            canvasUpdate[6].text = "Player: Caught";

        }
        else if (col.gameObject.name == "Cube") //changing coloyr of stuff when touched   
        {
            col.gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
   
    void seekFunction()
    {
        destination = targetObject.position;
        canvasUpdate[3].text = "Enemy: Seeking";
    }
    void patrolFunction()
    {
        if (Vector3.Distance(transform.position, destination) < 2.5)
        {
            destination = NextWaypoint(destination);
        }
        canvasUpdate[2].text = "Enemy: Patrolling";
    }
    void IdleFunction()
    {
        destination = NextWaypoint(destination);
        canvasUpdate[1].text = "Enemy: Idling";
    }

    
    public Vector3 NextWaypoint(Vector3 currentPosition) //patroling shiz
    {
        Debug.Log(currentPosition);
        if (currentPosition != Vector3.zero)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (currentPosition == waypoints[i].transform.position)
                {
                    nextIndex = (i + 1) % waypoints.Length;
                }
            }
        }
        else
        {
            nextIndex = 0;
        }
        return waypoints[nextIndex].transform.position;
    }

    
    public void isPlayerVisible() //VISIBLE STATE (RAYCAST)
    {

        // Create a vector from the enemy to the player and store the angle between it and forward.
        Vector3 direction = targetObject.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        // Create NavMesh hit var
        NavMeshHit hit;

        // If the Ray cast hits something other than the target, then true is returned, if not false
        // So !hit is used to specify visibility and...
        // If the angle between forward and where the player is, is less than half the angle of view...
        if (!navAgent.Raycast(targetObject.transform.position, out hit) && angle < fieldOfViewAngle * 0.5f)
        {
            // ... the player is Visible
            Debug.Log("Player is VISIBLE");
            isVisible = true;
            // Update Close Text on Canvas
            canvasUpdate[0].enabled = true;

        }
        else
        {
            // ... the player is Not Visible
            isVisible = false;
            Debug.Log("Player is NOT VISIBLE");
            // Update Close Text on Canvas
            canvasUpdate[0].enabled = false;
        }
    }

    
    public void isPlayerAudible() //AUDIO STATE
    {
        // If direct distance < 20, then audible
        if (Vector3.Distance(transform.position, targetObject.position) < 20.0f)
        {
            // Is Audible
            isAudible = true;
            // Update Close Text on Canvas
            canvasUpdate[1].enabled = true;
        }
        else
        {
            // Is not Audible
            isAudible = false;
            // Update Close Text on Canvas
            canvasUpdate[1].enabled = false;
        }
    }

    public void isPlayerClose() //PLAYER DISTANCE STATE
    {
        NavMeshPath path = new NavMeshPath();
        if (navAgent.enabled && navAgent.isOnNavMesh)
            navAgent.CalculatePath(targetObject.position, path);

        
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;

        allWayPoints[allWayPoints.Length - 1] = targetObject.position;
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0;

      
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        if (pathLength < 20.0f)
        {

            Debug.Log("Path Length: " + pathLength);

            // Set Close Bool true
            isClose = true;
            // Update Close Text on Canvas
            canvasUpdate[2].enabled = true;
        }
        else
        {

            // Set Close Bool false
            isClose = false;
            // Update Close Text on Canvas
            canvasUpdate[2].enabled = false;
        }
    }

}