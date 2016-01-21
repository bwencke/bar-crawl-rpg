using UnityEngine;
using System.Collections;

public class Child : ColliderController {
	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasBear")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Child", "1", null);
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Child", "1", AddBear);
		}
	}

	public void AddBear() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasBear")) {
			GameObject.Find("Bear").GetComponent<InventoryItemController>().Enable();
			GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio();
			GameObject.FindGameObjectWithTag("Alert").GetComponent<AlertController>().ShowStaticAlert("Bear added to inventory.");
		}
	}
}
