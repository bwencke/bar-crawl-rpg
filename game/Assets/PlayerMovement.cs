using UnityEngine;
using System.Collections;
using CnControls;

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

		float horizontal = 0;
		float vertical = 0;
	
//		if (count++ > 50) {
//			count = 0;
//			movement_vector = new Vector2 (Random.Range (1, 4) - 2, Random.Range (1, 4) - 2);
//		}

	#if UNITY_STANDALONE || UNITY_WEBPLAYER

		horizontal = (int) Input.GetAxisRaw ("Horizontal");
		vertical = (int) Input.GetAxisRaw ("Vertical");

	#else 

//		if(Input.touchCount > 0) {
//			Touch myTouch = Input.touches[0];
//			if(myTouch.phase == TouchPhase.Began) {
//				touchOrigin = myTouch.position;
//			} else if(myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0) {
//				Vector2 touchEnd = myTouch.position;
//				float x = touchEnd.x - touchOrigin.x;
//				float y = touchEnd.y - touchOrigin.y;
//				touchOrigin.x = -1;
//				if(Mathf.Abs(x) > Mathf.Abs(y)) {
//					horizontal = x > 0 ? 1 : -1;
//				} else {
//					vertical = y > 0 ? 1 : -1;
//				}
//			}
//		}

		horizontal = CnInputManager.GetAxisRaw("Horizontal");
		vertical = CnInputManager.GetAxisRaw("Vertical");

	#endif

		Vector2 movement_vector = new Vector2 (horizontal, vertical);
		
		if (movement_vector != Vector2.zero) {
			anim.SetBool ("is_walking", true);
			anim.SetFloat ("input_x", movement_vector.x);
			anim.SetFloat ("input_y", movement_vector.y);
		} else {
			anim.SetBool ("is_walking", false);
		}

		rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime);

		if (CnInputManager.GetButtonDown ("Jump")) {
			Debug.Log ("Button Pressed");

		}

	}
}
