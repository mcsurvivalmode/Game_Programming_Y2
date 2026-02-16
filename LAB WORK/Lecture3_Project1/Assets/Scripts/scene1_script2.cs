using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Scene1_Script2 : MonoBehaviour
{
	public GameObject playerSphere;
	// as the game becomes more complex, constants should be moved to data files (not PlayerPrefs)
	// MAX_MOVE_DISTANCE is the max speed the seek function can move
	const float MAX_MOVE_DISTANCE = 100.0f;
	// Deceleration factor is like a buffer around the target
	const float DECELERATION_FACTOR = 0.6f;
	//now variables needed by FixedUpdate
	float moveDistance;
	Vector3 source;
	Vector3 target;
	Vector3 outputVelocity;
	//and those for Seek
	Vector3 directionToTarget;
	Vector3 velocityToTarget;
	//and arrive
	float distanceToTarget;
	float speed;
	// Create an enum to control the movement type of the AI ball-
	// this will allow us to test both seek and arrive in the same script
	public enum MovementType { Seek, Arrive };
	public MovementType movementType;


	// Update is called once per frame
	void FixedUpdate ()
	{
		//We multiply by Time.deltaTime to ensure the same distance is achieved across different framerates
		moveDistance = MAX_MOVE_DISTANCE * Time.deltaTime;
		source = transform.position;
		//check to make sure player still exists!
		if (playerSphere != null) 
		{
			target = playerSphere.transform.position;
		} 
		else 
		{//move to the centre of the game area
			target = Vector3.zero;
		}
		
		// Run Seek Movement
		if (movementType == MovementType.Seek) 
		{
			outputVelocity = Seek (source, target, moveDistance);
			Debug.Log("Seeking");
		} 
		else if (movementType == MovementType.Arrive) 
		{
			outputVelocity = Arrive (source, target);
			Debug.Log("Arriving");
		}
	
		
		// Run Arrive Movement
		outputVelocity = Arrive (source, target);
		Debug.Log("Output Velocity" + outputVelocity);
		GetComponent<Rigidbody> ().AddForce (outputVelocity, ForceMode.VelocityChange);
	}

	// The Seek function purpose is to move an object from source to destination.
	// The function is called on each frame and returns a vector that defines the velocity
	// i.e a vector that defines direction and distance to cover per frame (displacement/time
	// with displacement being distance + direction)
	private Vector3 Seek (Vector3 source, Vector3 target, float moveDistance)
	{
		// Get direction to the target
		directionToTarget = Vector3.Normalize (target - source);
		// Calculate velocity along this line
		velocityToTarget = moveDistance * directionToTarget;
		// To Calculate the force to the target, subtract the objects current
		// movement from the from the force in the direction of the target
		return velocityToTarget - GetComponent<Rigidbody> ().linearVelocity;
	}
	
	// The Arrive function is similar to Seek but it also takes into account the distance
	// to the target and slows down as it gets closer to the target
	private Vector3 Arrive (Vector3 source, Vector3 target)
	{
		// Get the distance between source and target
		distanceToTarget = Vector3.Distance (source, target);
		// Get direction to the target
		directionToTarget = Vector3.Normalize (target - source);
		// Calculate current speed
		speed = distanceToTarget / DECELERATION_FACTOR;
		// Use Speed to control deceleration
		velocityToTarget = speed * directionToTarget;
		// To Calculate the force to the target, subtract the objects current
		// movement from the from the force in the direction of the target
		return velocityToTarget - GetComponent<Rigidbody> ().linearVelocity;
	}
}
