using UnityEngine;
using System.Collections;

public class IntroSequence : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Go ());
	}

	IEnumerator Go() {
		GameObject.FindGameObjectWithTag ("Title").GetComponent<Canvas> ().enabled = true;
		yield return new WaitForSeconds (1);
		GameObject.FindGameObjectWithTag ("BlackScreen").GetComponent<Canvas> ().enabled = false;
		GameObject.FindGameObjectWithTag ("Title").GetComponent<Canvas> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
