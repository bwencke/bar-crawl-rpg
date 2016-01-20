using UnityEngine;
using System.Collections;

public class NPCRandomMovement : MonoBehaviour {

	Rigidbody2D rbody;
	Animator anim;

	public float range = 0.1f;

	Vector2 movement_vector = Vector2.zero;
	Vector2 initial_position;

	bool paused;
	
	// Use this for initialization
	void Start () {
		
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		initial_position = rbody.position;

		Resume ();
		
	}

	public void SetInitialPosition() {
		initial_position = rbody.position;
	}

	public void Pause() {
		paused = true;
		Idle ();
	}

	public void Resume() {
		paused = false;
		Invoke("RandomDirection", Random.Range (0, 4));
	}
	
	void RandomDirection () {
		if (paused) {
			return;
		}

		float f = Random.Range (-1, 1);
		if (f < 0) {
			movement_vector = new Vector2 (Random.Range (1, 4) - 2, 0);
		} else {
			movement_vector = new Vector2 (0, Random.Range (1, 4) - 2);
		}

		NPCController npcc = GetComponent<NPCController> ();
		if (Mathf.Abs (rbody.position.x + movement_vector.x - initial_position.x) < range + 0.1 && Mathf.Abs (rbody.position.y + movement_vector.y - initial_position.y) < range + 0.1) {
			StartCoroutine (npcc.Move (movement_vector, 1.0f, 1.0f));
		}

		Invoke ("Idle", Random.Range (0.25f, 0.5f));

		Invoke ("RandomDirection", Random.Range (2, 4));

	}

	void Idle() {
		movement_vector = Vector2.zero;
	}
}
