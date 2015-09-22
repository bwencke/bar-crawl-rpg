using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rbody;
	Animator anim;

	// Use this for initialization
	void Start () {
	
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
//	int count = 0;
//	Vector2 movement_vector = new Vector2 (Random.Range (1, 4) - 2, Random.Range (1, 4) - 2);
	void Update () {
	
//		if (count++ > 50) {
//			count = 0;
//			movement_vector = new Vector2 (Random.Range (1, 4) - 2, Random.Range (1, 4) - 2);
//		}

		Vector2 movement_vector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (movement_vector != Vector2.zero) {
			anim.SetBool ("is_walking", true);
			anim.SetFloat ("input_x", movement_vector.x);
			anim.SetFloat ("input_y", movement_vector.y);
		} else {
			anim.SetBool ("is_walking", false);
		}

		rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime);

	}
}
