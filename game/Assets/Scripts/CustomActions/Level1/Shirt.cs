using UnityEngine;
using System.Collections;

public class Shirt : ColliderController {

	public override void TriggerPrimaryAction () {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		AlertController alertController = GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ();
		if(conversationController.dialogueEngine.checkVar ("BraydenAskedForHelp")) {
			alertController.ShowAlert ("This doesn't look like Brayden's style...");
		} else {
			alertController.ShowAlert ("Wow. Could this shirt be any uglier?");
		}
	}

}
