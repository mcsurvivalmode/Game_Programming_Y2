using UnityEngine;
using System.Collections;

public class Scene1_Script2 : MonoBehaviour
{
	public GameObject playerSphere;
	// as the game becomes more complex, constants should be moved to data files (not PlayerPrefs)
	// MAX_MOVE_DISTANCE is the max speed the seek function can move
	const float MAX_MOVE_DISTANCE = 200.0f;
	// Deceleration factor is like a buffer around the target, whe
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


	// Update is called once per frame
	void FixedUpdate ()
	{
		// Update DF from Scriptabe Objects
		//DECELERATION_FACTOR = gm.gameStatus.DE
		moveDistance = MAX_MOVE_DISTANCE * Time.deltaTime;
		source = transform.position;
		//check to make sure player still exists!
		if (playerSphere != null) {
			target = playerSphere.transform.position;
		} else {//move to the centre of the game area
			target = Vector3.zero;
		}
		// Run Seek Movement
		outputVelocity = Seek (source, target, moveDistance);
		// Run Arruve Movement
		//outputVelocity = Arrive (source, target);
		GetComponent<Rigidbody> ().AddForce (outputVelocity, ForceMode.VelocityChange);
	}

	//seek function
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

	//arrive function
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
