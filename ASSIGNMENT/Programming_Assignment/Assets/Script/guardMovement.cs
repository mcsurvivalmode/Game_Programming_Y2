using UnityEngine;
using System.Collections;

public class guardMovement : MonoBehaviour
{
    // Declare a public variable to reference the Main Camera
    public GameObject mainCamera;
    public const float PROXIMITY_DISTANCE = 1.0f;
    private GameObject currentTarget;
    private waypointScript waypointManager;
    const float DECELERATION_FACTOR = 0.6f;

    // Now variables needed by FixedUpdate
    Vector3 source;
    Vector3 target;
    Vector3 outputVelocity;

    // And arrive
    Vector3 directionToTarget;
    Vector3 velocityToTarget;
    float distanceToTarget;
    float speed;

    // Use this for initialisation
    void Awake()
    {
        // Get the WaypointManager from the camera and then the first object
        waypointManager = mainCamera.GetComponent<waypointScript>();
        currentTarget = waypointManager.NextWaypoint(null);
    }

    void FixedUpdate()
    {
        source = transform.position;
        target = currentTarget.transform.position;
        outputVelocity = Arrive(source, target);
        GetComponent<Rigidbody>().AddForce(outputVelocity, ForceMode.VelocityChange);
        // Check the distance from object to target, and make query
        // When it moves within the PROXIMITY_DISTANCE
        if (Vector3.Distance(source, target) < PROXIMITY_DISTANCE)
        {
            currentTarget = waypointManager.NextWaypoint(currentTarget);
        }
    }

    // Arrive function
    private Vector3 Arrive(Vector3 source, Vector3 target)
    {
        distanceToTarget = Vector3.Distance(source, target);
        directionToTarget = Vector3.Normalize(target - source);
        speed = distanceToTarget / DECELERATION_FACTOR;
        velocityToTarget = speed * directionToTarget;
        return velocityToTarget - GetComponent<Rigidbody>().linearVelocity;
    }
}
