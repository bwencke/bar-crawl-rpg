using UnityEngine;
using System.Collections;

public class BlackBarSlider : MonoBehaviour {
	
	Animator anim;
	bool isSliding = false;
	
	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator> ();
		
	}
	
	void AnimationComplete() {
		isSliding = false;
	}
	
	public IEnumerator SlideOut() {
		anim.ResetTrigger ("slideIn");
		isSliding = true;
		anim.SetTrigger ("slideOut");
		
		while (isSliding) {
			yield return null;
		}
	}
	
	public IEnumerator SlideIn() {
		anim.ResetTrigger ("slideOut");
		isSliding = true;
		anim.SetTrigger ("slideIn");

		while (isSliding) {
			yield return null;
		}
	}
}
