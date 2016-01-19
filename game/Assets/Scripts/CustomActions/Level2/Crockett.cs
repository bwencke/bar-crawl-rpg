using UnityEngine;
using System.Collections;

public class Crockett : ColliderController {
	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasHat")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Crockett", "1", null);
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Crockett", "1", AddHat);
		}
	}

	public void AddHat() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasHat")) {
			conversationController.dialogueEngine.setVar ("HasBear", false);
			GameObject.Find("Bear").GetComponent<InventoryItemController>().Disable();
			GameObject.Find("Hat").GetComponent<InventoryItemController>().Enable();
			GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio();
			GameObject.FindGameObjectWithTag("Alert").GetComponent<AlertController>().ShowStaticAlert("Hat added to your inventory!");
		}
	}
}
