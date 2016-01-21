using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tail : InventoryItemController {

	public Image playerImage;

	public override void UseItemOnSelf() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		conversationController.dialogueEngine.setVar ("WearingMoustache", true);
		GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio ();

		if (conversationController.dialogueEngine.checkVar ("WearingSunglasses")) {
			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowStaticAlert("Now I look exactly like Harvey Kevork.");
			playerImage.sprite = (Sprite) Resources.Load ("Images/BlakeDisguise", typeof(Sprite));
		} else {
			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowStaticAlert("The moustache is on. Itchiness: +200");
			playerImage.sprite = (Sprite) Resources.Load ("Images/BlakeMoustache", typeof(Sprite));
		}

		Disable ();
	}
}
