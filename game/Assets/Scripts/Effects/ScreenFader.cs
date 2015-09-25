using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	Animator anim;
	bool isFading = false;

	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animator> ();

	}

	void AnimationComplete() {
		isFading = false;
	}

	public IEnumerator FadeToClear() {
		isFading = true;
		anim.SetTrigger ("fadeIn");

		while (isFading) {
			yield return null;
		}
	}

	public IEnumerator FadeToBlack() {
		isFading = true;
		anim.SetTrigger ("fadeOut");
		
		while (isFading) {
			yield return null;
		}
	}
}
