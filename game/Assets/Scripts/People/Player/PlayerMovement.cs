using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CnControls;

public class PlayerMovement : TopLevelController {

	public GoogleAnalyticsV3 googleAnalytics;

	Rigidbody2D rbody;
	Animator anim;

	public string currentMap = "map_outside";

	bool isRunning;
	float lastTime;
	KeyCode lastKeyCode;
	
	Vector2 direction = new Vector2(0, 1);

	// Use this for initialization
	void Start () {
	
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		isRunning = false;
		lastTime = -1.0f;

	}

	public override void TriggerMovement(Vector2 movement_vector) {

		Vector2 prevDirection = new Vector2 (direction.x, direction.y);
		if (movement_vector != Vector2.zero) {
			anim.SetBool ("is_walking", true);
			anim.SetFloat ("input_x", movement_vector.x);
			anim.SetFloat ("input_y", movement_vector.y);
			if(movement_vector.x != 0) {
				direction.x = movement_vector.x < 0 ? -1 : 1;
			} else {
				direction.x = 0;
			}
			if(movement_vector.y != 0) {
				direction.y = movement_vector.y < 0 ? -1 : 1;
			} else {
				direction.y = 0;
			}
		} else {
			anim.SetBool ("is_walking", false);
		}

		rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime * (isRunning ? 2 : 1));

		if (movement_vector != Vector2.zero) {
			GameObject.FindGameObjectWithTag("Alert").GetComponent<AlertController>().CancelAlert();
		}

	}
	
	public override void TriggerPrimaryAction(System.Action<string> callback) {
		RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position, direction, .2f);
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider != null && hit.collider.gameObject.name != "Player") {
				Debug.Log (hit.collider.gameObject.name);
				//Debug.Log("You can't do anything with this object!");
				hit.collider.gameObject.GetComponent<ColliderController>().TriggerPrimaryAction();
				return;
			}
		}
		GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert ("It is just empty space.");
	}

	public override void KeyDown() {
		if (Input.GetKey (lastKeyCode)) {
			if (Time.time - lastTime < 0.2f) {
				lastTime = Time.time;
				isRunning = true;
				return;
			}
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			lastKeyCode = KeyCode.LeftArrow;
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			lastKeyCode = KeyCode.RightArrow;
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			lastKeyCode = KeyCode.UpArrow;
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			lastKeyCode = KeyCode.DownArrow;
		}
		lastTime = Time.time;
		isRunning = false;
	}

}
