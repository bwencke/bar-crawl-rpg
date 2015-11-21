using UnityEngine;
using System.Collections;

public class NPCController : ColliderController {

	public GoogleAnalyticsV3 googleAnalytics;

	public string npcName;
	public string conversationId;

	Rigidbody2D rbody;
	Animator anim;

	Vector2 movement_vector;

	void Start() {

		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		movement_vector = Vector2.zero;

		gameObject.GetComponent<Animator>().SetFloat("input_x", 0);
		gameObject.GetComponent<Animator>().SetFloat("input_y", -1);
		gameObject.GetComponent<Animator>().SetBool("is_walking", false);
	}

	void Update() {

		if (movement_vector != Vector2.zero) {
			anim.SetBool ("is_walking", true);
			anim.SetFloat ("input_x", movement_vector.x);
			anim.SetFloat ("input_y", movement_vector.y);
		} else {
			anim.SetBool("is_walking", false);
		}

		rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime);
	}

	public override void TriggerPrimaryAction() {
		googleAnalytics.LogEvent (new EventHitBuilder ()
		                         .SetEventCategory ("Primary Action")
		                         .SetEventAction ("Talk to NPC")
		                         .SetEventLabel (npcName));
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller>().StartConversation (npcName, conversationId, null);
	}

	public IEnumerator MoveDown(float distance) {
		yield return StartCoroutine (Move (new Vector2 (0, -1), distance));
	}

	public IEnumerator MoveLeft(float distance) {
		yield return StartCoroutine (Move (new Vector2 (-1, 0), distance));
	}

	public IEnumerator MoveUp(float distance) {
		yield return StartCoroutine (Move (new Vector2 (0, 1), distance));
	}
	
	public IEnumerator MoveRight(float distance) {
		yield return StartCoroutine (Move (new Vector2 (1, 0), distance));
	}

	public IEnumerator Move(Vector2 direction, float distance) {
		Vector2 initial_position = rbody.position;
		movement_vector = direction*1.5f;
		while (Mathf.Abs(initial_position.x - rbody.position.x) < distance*.3f &&
		       (Mathf.Abs(initial_position.y - rbody.position.y) < distance*.3f)) {
			yield return null;
		}
		movement_vector = new Vector2 (0, 0);
	}

	public void SetPosition(Vector2 position) {
		rbody.position = position;
	}

	public void SetDirection(Vector2 direction) {
		anim.SetFloat ("input_x", direction.x);
		anim.SetFloat ("input_y", direction.y);
	}
}
