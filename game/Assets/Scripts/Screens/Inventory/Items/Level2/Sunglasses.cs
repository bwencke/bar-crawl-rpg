using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sunglasses : InventoryItemController {

	public Image playerImage;

	public override void UseItemOnSelf() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		conversationController.dialogueEngine.setVar ("WearingSunglasses", true);
		GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio ();

		if (conversationController.dialogueEngine.checkVar ("WearingMoustache")) {
			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowStaticAlert("Congratulations! Your disguise is complete!");
			playerImage.sprite = (Sprite) Resources.Load ("Images/BlakeDisguise", typeof(Sprite));
		} else {
			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowStaticAlert("You put the sunglasses on. Coolness: +200");
			playerImage.sprite = (Sprite) Resources.Load ("Images/BlakeSunglasses", typeof(Sprite));
		}

		Disable ();
	}
}
