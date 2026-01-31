using UnityEngine;
using System.Collections;

public class Scene4_Script2 : MonoBehaviour {

	// Declare Animator Object
	Animator anim;

	// Use this for initialization
	void Start () {

		// Instantiate Animator Object 
		anim = GetComponent<Animator> ();
	}
		
	// This fucntion is triggered each time the collider interacts with Teddy
	void OnTriggerEnter(Collider other){

		// Ensure door only Opens for Teddy
		if (other.gameObject.name == "Teddy" && anim.GetBool("doorOpen") == false) {

			// Set doorOpen bool to true
			// This triggers DoorSlideUp animation 
			anim.SetBool ("doorOpen", true);
			Debug.Log ("Door Open:" + other.gameObject.name);
		}
	}

	// This fucntion is triggered each time the collider exits interaction with Teddy
	void OnTriggerExit(Collider other){
		// Ensure door only Closes for Teddy
		if (other.gameObject.name == "Teddy") {

			// Set doorOpen bool to true
			// This triggers DoorSlideDown animation 
			anim.SetBool ("doorOpen", false);
			Debug.Log ("Door Close:" + other.gameObject.name);
		} 
	}
}
