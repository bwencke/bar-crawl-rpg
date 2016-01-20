using UnityEngine;
using System.Collections;

public class Gambler : ColliderController {
	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasDrink")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Gambler", "1", null);
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Gambler", "1", HasDrink);
		}
	}

	public void HasDrink() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasDrink")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().LoadCutscene ("Win");
		}
	}
}
