using UnityEngine;
using System.Collections;

public class behaviour : StateMachineBehaviour {

	// Create AudioSource array to hold all AudioSource Comnponents
	private AudioSource[] audio;

	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		audio = animator.GetComponentsInParent<AudioSource> ();
	
		audio [0].Play ();
	}

    	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		audio = animator.GetComponentsInParent<AudioSource> ();
		
		audio [0].Stop ();
	}

}
