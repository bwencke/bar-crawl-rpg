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

		Pause ();
		
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

	void Update() {

		Vector2 newPosition = rbody.position + movement_vector * Time.deltaTime;

		if (range > 0) {
			if (Mathf.Abs (newPosition.x - initial_position.x) > range || Mathf.Abs (newPosition.y - initial_position.y) > range) {
				movement_vector = Vector2.zero;
			}
		}
	
		if (movement_vector != Vector2.zero) {
			if(range > 0) {
				anim.SetBool ("is_walking", true);
			}
			anim.SetFloat ("input_x", movement_vector.x);
			anim.SetFloat ("input_y", movement_vector.y);
		} else {
			anim.SetBool("is_walking", false);
		}

		if (range > 0) {
			rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime);
		}
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

		Invoke ("Idle", Random.Range (.25f, .5f));

		Invoke ("RandomDirection", Random.Range (2, 4));

	}

	void Idle() {
		movement_vector = Vector2.zero;
	}
}
