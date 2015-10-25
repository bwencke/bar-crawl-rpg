using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CnControls;

public class PlayerMovement : TopLevelController {

	public GoogleAnalyticsV3 googleAnalytics;

	Rigidbody2D rbody;
	Animator anim;
	
	Vector2 direction = new Vector2(0, 1);

	// Use this for initialization
	void Start () {
	
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

	}

	public override void TriggerMovement(Vector2 movement_vector) {
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
		
		rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime);

		if (movement_vector != Vector2.zero) {
			GameObject.FindGameObjectWithTag("Alert").GetComponent<AlertController>().CancelAlert();
		}

	}
	
	public override void TriggerPrimaryAction() {
		RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position, direction, .3f);
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider != null && hit.collider.gameObject.name != "Player") {
				//Debug.Log("You can't do anything with this object!");
				hit.collider.gameObject.GetComponent<ColliderController>().TriggerPrimaryAction();
			}
		}
	}

}
