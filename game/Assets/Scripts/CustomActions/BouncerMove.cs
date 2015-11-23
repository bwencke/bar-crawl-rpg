using UnityEngine;
using System.Collections;

public class BouncerMove : ColliderController {
	public NPCController Bouncer;

	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("ShowedID")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Bouncer", "1", null);
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Bouncer", "1", move);
		}
	}

	public void move() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("ShowedID")) {
			StartCoroutine(actuallyMove());
		}
	}

	public IEnumerator actuallyMove() {
		yield return StartCoroutine(Bouncer.MoveDown(0.1f));
		yield return StartCoroutine(Bouncer.MoveLeft(1.5f));
		Bouncer.SetDirection(new Vector2(0, -1));
	}
}
