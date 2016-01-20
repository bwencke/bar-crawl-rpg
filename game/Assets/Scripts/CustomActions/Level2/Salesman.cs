using UnityEngine;
using System.Collections;

public class Salesman : ColliderController {
	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasFakeID")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Salesman", "1", null);
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Salesman", "1", AddFakeID);
		}
	}

	public void AddFakeID() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasFakeID")) {
			conversationController.dialogueEngine.setVar ("HasMoney", false);
			GameObject.Find("Money").GetComponent<InventoryItemController>().Disable();
			GameObject.Find("Fake ID").GetComponent<InventoryItemController>().Enable();
			GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio();
			GameObject.FindGameObjectWithTag("Alert").GetComponent<AlertController>().ShowStaticAlert("Fake ID added to your inventory!");
		}
	}
}
