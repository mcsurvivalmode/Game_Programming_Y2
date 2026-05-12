using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;


public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent navAgent;
    Vector3 destination;

    //canvas update on state!
   

    public bool isVisible;
    public bool isAudible;
    public bool isClose;
    public bool isHome;

    //so enemy follows the player
    public Transform targetObject;
    Animator anim; 
   
    Vector3 worldDeltaPosition;
    Vector2 groundDeltaPosition;
    Vector2 velocity = Vector2.zero;

    CharacterMovement stateCheck;

    

  
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

        stateCheck = FindAnyObjectByType<CharacterMovement>();
    }

    
    void Update() //decision tree 
    {

             //this will be a state for if the player reaches home, the enemy cannot look for them anymore
        if (stateCheck == true) 
        {
            //PatrolSafeFunction();
            isHome = true;
        }
        else
        {
            isHome = false;
        }

        if (targetObject)
        {
            IsPlayerVisible();
            IsPlayerAudible();
            IsPlayerClose();
            




            if (isVisible && isClose)
            {
                SeekFunction(); //is visible and near 
            }
            else if (isVisible && !isClose)
            {
                PatrolFunction(); //is visible and not near 
            }
            else if (!isVisible && !isAudible)
            {
                IdleFunction(); //isnt visible or close 
            }
            else if (!isVisible && isAudible)
            {
                PatrolFunction();
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
      
    }


    void OnAnimatorMove()
    {
        transform.position = navAgent.nextPosition;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "player")
        {
            //CanvasUpdate[6].text = "Player: Caught";

        }
    }
   
    void SeekFunction()
    {
        destination = targetObject.position;
        
    }
    void PatrolFunction()
    {
        if (Vector3.Distance(transform.position, destination) < 2.5)
        {
            destination = NextWaypoint(destination);
        }
        
    }
    void IdleFunction()
    {
        destination = NextWaypoint(destination);
       
    }
    
    //void PatrolSafeFunction()
    //{
        //if (Vector3.Distance(transform.position, destination) < 2.5)
        //{
            //destination = NextWaypoint(destination);
        //}
        //CanvasUpdate[4].text = "Enemy: Patroling(player safe)";
    //}
    public Vector3 NextWaypoint(Vector3 currentPosition) //patroling shiz
    {
      
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

    
    public void IsPlayerVisible() //VISIBLE STATE (RAYCAST)
    {

     
        Vector3 direction = targetObject.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

      
        NavMeshHit hit;

    
        if (!navAgent.Raycast(targetObject.transform.position, out hit) && angle < fieldOfViewAngle * 0.5f)
        {
            // ... the player is Visible
          
            isVisible = true;
            // Update Close Text on Canvas
            

        }
        else
        {
            // ... the player is Not Visible
            isVisible = false;
           
            // Update Close Text on Canvas
            
        }
    }

    
   

    public void IsPlayerAudible() //AUDIO STATE
    {
        // If direct distance < 20, then audible
        if (Vector3.Distance(transform.position, targetObject.position) < 20.0f)
        {
            // Is Audible
            isAudible = true;
            // Update Close Text on Canvas
            
        }
        else
        {
            // Is not Audible
            isAudible = false;
            // Update Close Text on Canvas
            
        }
    }

    public void IsPlayerClose() //PLAYER DISTANCE STATE
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

            

            // Set Close Bool true
            isClose = true;
          
        }
        else
        {

            // Set Close Bool false
            isClose = false;
           
        }
    }

}