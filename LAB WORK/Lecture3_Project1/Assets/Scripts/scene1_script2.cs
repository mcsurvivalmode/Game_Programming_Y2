using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class scene1_script2 : MonoBehaviour
{
    public GameObject playerSphere;

    const float MAX_MOVE_DISTANCE = 100.0f;
    const float DECELERATION_FACTOR = 0.6f;

    //variables for fixed update
    float moveDistance;
    Vector3 source;
    Vector3 target;
    Vector3 outputVelocity;
    //seek
    Vector3 directionToTarget;
    Vector3 velocityToTarget;
    //arrive
    float distanceToTarget;
    float speed;

    //using both in same script 
    public enum MovementType { Seek, Arrive};
    public MovementType movementType;


    void FixedUpdate()
    {
        moveDistance = MAX_MOVE_DISTANCE * Time.deltaTime;
        source = Transform.position;
        //check player exists 
        if (playerSphere != null)
        {
            target = playerSphere.transform.position;
        }
        else
        {
            target = Vector3.Zero;
        }
        //run seek movement
        if (movementType == MovementType.Seek)
        {
            outputVelocity = SeekOrigin (source, target, moveDistance);
            Debug.Log("Seeking");
        }
        else if (movementType == MovementType.Arrive)
        {
            outputVelocity = Arrive (source, target);
            Debug.Log("Arrivin");
        }
        //arrive movement
        Debug.Log("Output Velocity" + outputVelocity);
        GetComponent<Rigidbody> ().AddForce (outputVelocity, ForceMode.VolocityChange);
    }
    //seek function - move object to destnation 
    private Vector3 Seek(Vector3 source, Vector3 target, float moveDistance)
    {
        //get direction 
        directionToTarget = Vector3.Normalize (target - source);
        //velocity along this line
        velocityToTarget = moveDistance * directionToTarget;
        //calculate force 
        return velocityToTarget - GetComponent<Rigidbody> ().linearVelocity;
    }

    //arrive function 
    private Vector3 Arrive (Vector3 source, Vector3 target)
    {
        directionToTarget = Vector3.Distance (source,target);
        directionToTarget = Vector3.Normalize (target - source);
        speed = distanceToTarget / DECELERATION_FACTOR;
        velocityToTarget = speed * directionToTarget;

        return velocityToTarget - GetComponent<Rigidbody> ().linearVelocity;
    }
 
}
