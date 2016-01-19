using UnityEngine;
using System.Collections;

public class Butcher : ColliderController {
	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasTail")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Butcher", "1", null);
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Butcher", "1", AddTail);
		}
	}

	public void AddTail() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasTail")) {
			conversationController.dialogueEngine.setVar ("HasHat", false);
			GameObject.Find("Hat").GetComponent<InventoryItemController>().Disable();
			GameObject.Find("Tail").GetComponent<InventoryItemController>().Enable();
			GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio();
			GameObject.FindGameObjectWithTag("Alert").GetComponent<AlertController>().ShowStaticAlert("Tail added to your inventory!");
		}
	}
}
