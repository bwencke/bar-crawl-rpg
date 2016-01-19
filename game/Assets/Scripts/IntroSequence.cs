using UnityEngine;
using System.Collections;

public class IntroSequence : MonoBehaviour {

	public StateController stateController;

	// Use this for initialization
	void Start () {
		StartCoroutine(Go ());
	}

	IEnumerator Go() {
		GameObject.FindGameObjectWithTag ("Title").GetComponent<Canvas> ().enabled = true;
		yield return new WaitForSeconds (2.5f);
		GameObject.FindGameObjectWithTag ("Title").GetComponent<Canvas> ().enabled = false;
		stateController.Begin ();
	}
}
