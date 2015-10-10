using UnityEngine;
using System.Collections;

public class BubbleDestroyer : MonoBehaviour {

	public BubbleGenerator bubbleGenerator;

	void OnTriggerEnter2D(Collider2D other) {
		Destroy(other.gameObject);
		bubbleGenerator.Destroyed ();
	}

}
