using UnityEngine;
using System.Collections;

public class Level2BouncerMove : ColliderController {
	public NPCController Bouncer;
	
	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("FakeIDSuccess")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Bouncer", "1", null);
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Bouncer", "1", move);
		}
	}
	
	public void move() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("FakeIDSuccess")) {
			GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio();
			StartCoroutine(actuallyMove());
		}
	}
	
	public IEnumerator actuallyMove() {
		yield return StartCoroutine(Bouncer.MoveLeft(1f));
		Bouncer.SetDirection(new Vector2(0, -1));
	}
}
