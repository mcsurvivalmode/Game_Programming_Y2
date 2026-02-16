using UnityEngine;

public class Scene1_Script1 : MonoBehaviour
{
	//declare a public variable to reference the Main Camera
	public GameObject textUI;
	//now input control axes
	float verticalMovement;
	float horizontalMovement;
	// OnCollisionEnter will trigger when a collision begins
	void OnCollisionEnter (Collision col)
	{
		// the collision will return the gameObject itself- the name property allows different
		//hitting a cube benefits the economy!
		if (col.gameObject.name == "Wall") 
		{
			// change the colour of the object
			col.gameObject.GetComponent<Renderer> ().material.color = Color.green;
			//now update the state data
			textUI.GetComponent<Scene1_Script3> ().Economy += 1;
		}
		else 
		{
			//hitting anything else is bad for our health!!
			if (col.gameObject.name == "AIBall") 
			{
				Destroy (col.gameObject);
				//reduce health level
				textUI.GetComponent<Scene1_Script3> ().HealthLevel -= 25;
			}
		}
	}//end of collision condition

	// Update is called once per frame
	void FixedUpdate ()
	{
		// Check current health level to determine whether player must die!
		if (textUI.GetComponent<Scene1_Script3> ().HealthLevel <= 0) 
		{
			// MonoBehaviour has a gameObject property for the current game object
			Destroy (gameObject);
		} 
		else 
		{
			// Get the input values for the horizontal and vertical axes
			verticalMovement = Input.GetAxis ("Vertical");
			horizontalMovement = Input.GetAxis ("Horizontal");
			// Now a compound if statement to determine the direction of the vector
			Vector3 myDirectionVector = new Vector3 ();
			if (verticalMovement > 0) 
			{
				myDirectionVector = Vector3.forward * verticalMovement;
			} 
			else if (verticalMovement < 0) 
			{
				myDirectionVector = Vector3.back * -verticalMovement;
			} 
			else if (horizontalMovement > 0) 
			{
				myDirectionVector = Vector3.right * horizontalMovement;
			} 
			else if (horizontalMovement < 0) 
			{
				myDirectionVector = Vector3.left * -horizontalMovement;
			}
			// Add force to the sphere to move it-
			GetComponent<Rigidbody> ().AddForce (myDirectionVector / 5, ForceMode.Impulse);
		}
	}
}
