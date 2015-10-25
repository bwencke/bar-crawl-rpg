using UnityEngine;
using System.Collections;

public class BubbleMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating("ChangeDirection", 0, 2);
	}

	void ChangeDirection () {
		Vector2 vector = Vector2.zero;
		Vector2 velocity = gameObject.GetComponent<Rigidbody2D> ().velocity;
		gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.SmoothDamp (velocity, new Vector2 (Random.Range (-5, 5), velocity.y), ref vector, .1f);
	}

}
