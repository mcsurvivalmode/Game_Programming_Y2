using UnityEngine;
using System.Collections;

public class Scene2_Behaviour1 : StateMachineBehaviour {

	// Create AudioSource array to hold all AudioSource Comnponents
	private AudioSource[] audio;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (animator.GetLayerWeight(layerIndex) == 1) {
			// Get access to GetComponentsInParent, this will return an array
			audio = animator.GetComponentsInParent<AudioSource> ();
			// PLay audio file at index 0 (hello)
			audio[0].Play();
		}
	}
		
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (animator.GetLayerWeight(layerIndex) == 0) {
			// Get access to GetComponentsInParent, this will return an array
			audio = animator.GetComponentsInParent<AudioSource> ();
			// Stop audio file[0] (hello)
			audio[0].Stop ();
		}

	
	}
}
