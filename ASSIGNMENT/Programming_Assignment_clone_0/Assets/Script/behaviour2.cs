using UnityEngine;
using System.Collections;

public class behaviour2 : StateMachineBehaviour {

	// Create AudioSource array to hold all AudioSource Comnponents
	private AudioSource[] audio;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		// Get access to GetComponentsInParent, this will return an array
		audio = animator.GetComponentsInParent<AudioSource> ();
		// PLay audio file at index 0 (Door open)
		audio [1].Play ();
	}

    	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		// Get access to GetComponentsInParent, this will return an array
		audio = animator.GetComponentsInParent<AudioSource> ();
		// PLay audio file at index 0 (Door open)
		audio [1].Stop ();
	}

}