using UnityEngine;
using System.Collections;

public class BankerDesk : ColliderController {
	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasMoney")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Banker", "1", null);
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Banker", "1", AddMoney);
		}
	}

	public void AddMoney() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasMoney")) {
			GameObject.FindGameObjectWithTag("Money").GetComponent<InventoryItemController>().Enable();
			GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio();
			GameObject.FindGameObjectWithTag("Alert").GetComponent<AlertController>().ShowStaticAlert("Money added to inventory.");
		}
	}
}
