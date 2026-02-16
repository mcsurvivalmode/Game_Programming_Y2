using UnityEngine;

public class Scene3_Script1 : MonoBehaviour
{
	// Source position of player
	Vector3 source;
	// Target Position of Player
	Vector3 target;
	// Speed of travel
	float speed;
	// Distance between source and target
	float distanceToTarget;
	// Deceleration constant value
	const float DECELERATION_FACTOR = 0.6f;
	void FixedUpdate ()
	{
		source = transform.position;
		// Detect Mouse Click
		if (Input.GetMouseButtonDown(0))
		{
			// Create RayCastHit object
			RaycastHit hit;
			// Create Ray to determine where click occurred in world space
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				// Make target == hit point
				target = hit.point;
			}
		}
		// Call FollowPoint function and pass in source and target
		this.transform.position = FollowPoint (source, target);
	}

	// FollowPoint function
	private Vector3 FollowPoint (Vector3 source, Vector3 target)
	{
		// Get distance between source and target
		distanceToTarget = Vector3.Distance (source, target);
		// Determine speed based on distance and deceleration factor
		speed = distanceToTarget / DECELERATION_FACTOR;
		// return position to follow to
		return Vector3.MoveTowards(source, target, Time.deltaTime * speed);
	}
}
