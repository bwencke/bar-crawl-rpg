﻿using UnityEngine;
using System.Collections;

public class IntroSequence : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Go ());
	}

	IEnumerator Go() {
		GameObject.FindGameObjectWithTag ("Title").GetComponent<Canvas> ().enabled = true;
		yield return new WaitForSeconds (1);
		GameObject.FindGameObjectWithTag ("Title").GetComponent<Canvas> ().enabled = false;
	}
}
