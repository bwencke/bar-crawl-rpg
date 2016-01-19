using UnityEngine;
using System.Collections;

public class BraydenController : ColliderController {
	public NPCController Brayden;

	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("BraydenIDReturned")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Brayden", "1", null);
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Brayden", "1", RemoveID);
		}
	}

	public void RemoveID() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("BraydenIDReturned")) {
			GameObject.FindGameObjectWithTag("BraydensID").GetComponent<InventoryItemController>().Disable();
			GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio();
			StartCoroutine(actuallyMove());
		}
	}

	public IEnumerator actuallyMove() {
		yield return StartCoroutine(Brayden.MoveDown(0.1f));
		yield return StartCoroutine(Brayden.MoveLeft(2f));
		yield return StartCoroutine(Brayden.MoveUp(1f));
		GameObject.FindGameObjectWithTag ("DoorSound").GetComponent<AudioObject> ().PlayAudio();
		Brayden.SetPosition (new Vector2 (-33.117f, 0.657f));
		Brayden.SetDirection(new Vector2(-1, 0));
	}
}
