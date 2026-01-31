using UnityEngine;
using System.Collections;

public class Scene2_Script1 : MonoBehaviour {

	// Setup a variable to point to the Animator Controller for the character
	Animator animator;
	// Setup 2 float for vertical/horizontal input
	float verticalInput;
	float horizontalInput;

	void Start () {
		//get the Animator Controller Component from the character component hierarchy
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		// Get the input from vertical/horizontal axis
		verticalInput = Input.GetAxis("Vertical");
		horizontalInput = Input.GetAxis("Horizontal");
	}
	void FixedUpdate(){
		// Now set the animator float values (vAxisInput/hAxisInput)
		animator.SetFloat ("vAxisInput", verticalInput);
		animator.SetFloat ("hAxisInput", horizontalInput);

		// Detect Z Key press
		if (Input.GetKey (KeyCode.Z)) {
			// Set runBool to true if pressed
			animator.SetBool ("runBool", true);
			Debug.Log ("Run");

		} else {
			// Set runBool to false if not pressed
			animator.SetBool ("runBool", false);
			Debug.Log ("No Run");
		}

		// Detect X Key press
		if (Input.GetKey (KeyCode.X)) {
			// Set the Crouch Layer Weight to 0.5, this 
			// activtes the masked couch animation
			animator.SetLayerWeight (1, 1.0f);

		} else {
			// Set the Couch Layer Weight back to 0.0
			// This deactivated the crouch animation
			animator.SetLayerWeight (1, 0.0f);
		}
	}
}