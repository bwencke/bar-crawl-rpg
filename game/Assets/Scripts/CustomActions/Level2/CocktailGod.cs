using UnityEngine;
using System.Collections;

public class CocktailGod : ColliderController {
	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasSunglasses")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("CocktailGod", "1", null);
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("CocktailGod", "1", AddSunglasses);
		}
	}

	public void AddSunglasses() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasSunglasses")) {
			GameObject.Find("Sunglasses").GetComponent<InventoryItemController>().Enable();
			GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio();
			GameObject.FindGameObjectWithTag("Alert").GetComponent<AlertController>().ShowStaticAlert("Sunglasses added to inventory.");
			GameObject.Find ("StateController").GetComponent<StateController> ().npcImageMap.SetImage ("Cocktail God", "Cocktail God");
		}
	}
}
