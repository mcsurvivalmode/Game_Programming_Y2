using UnityEngine;
using System.Collections;

public class behaviour2 : StateMachineBehaviour {

	
	private AudioSource[] audio;

	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		audio = animator.GetComponentsInParent<AudioSource> ();
		
		audio [1].Play ();
	}

    	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		
		audio = animator.GetComponentsInParent<AudioSource> ();
	
		audio [1].Stop ();
	}

}