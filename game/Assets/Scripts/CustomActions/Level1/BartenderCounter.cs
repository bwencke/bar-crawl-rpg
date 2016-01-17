using UnityEngine;
using System.Collections;

public class BartenderCounter : ColliderController {

	public override void TriggerPrimaryAction () {
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation("Bartender", "1", HasDrink);
	}

	public void HasDrink() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasDrink")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().LoadCutscene ("Win");
		}
	}
}
