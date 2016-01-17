using UnityEngine;
using System.Collections;

public class THEWasher : ColliderController {

	public InventoryItemController braydensID;
	public InventoryItemController washingMachine;

	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		AlertController alertController = GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ();
		if (!conversationController.dialogueEngine.checkVar ("FoundID")) {
			if(conversationController.dialogueEngine.checkVar ("BraydenAskedForHelp")) {
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation("washer", "1", addItems);
			} else {
				alertController.ShowAlert ("I probably shouldn't be going through somebody's clothes.");
			}	
		} else {
			alertController.ShowAlert ("It's empty.");
		}
	}

	public void addItems() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		AlertController alertController = GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ();
		GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio ();
		alertController.ShowStaticAlert ("Brayden's ID and a washing machine added to inventory!");
		conversationController.dialogueEngine.setVar ("FoundID", true);
		Destroy(GameObject.FindGameObjectWithTag ("THEWasher"));
		braydensID.Enable();
		washingMachine.Enable();
	}
	
}
