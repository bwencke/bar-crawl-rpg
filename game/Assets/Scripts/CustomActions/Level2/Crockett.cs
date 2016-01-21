using UnityEngine;
using System.Collections;

public class Crockett : ColliderController {
	
	public override void TriggerPrimaryAction () {
		GameObject.Find ("Crockett").GetComponent<NPCRandomMovement> ().Pause ();
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasHat")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Crockett", "1", null);
			GameObject.Find ("Crockett").GetComponent<NPCRandomMovement> ().Resume ();
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Crockett", "1", AddHat);
		}
	}

	public void AddHat() {
		ConversationController conversationController = GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ();
		if (conversationController.dialogueEngine.checkVar ("HasHat")) {
			// Note: Don't set this variable to false; otherise, you can get the bear from the child again
			// conversationController.dialogueEngine.setVar ("HasBear", false);
			GameObject.Find ("Bear").GetComponent<InventoryItemController> ().Disable ();
			GameObject.Find ("Hat").GetComponent<InventoryItemController> ().Enable ();
			GameObject.FindGameObjectWithTag ("SuccessChime").GetComponent<AudioObject> ().PlayAudio ();
			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowStaticAlert ("Hat added to inventory.");
			StartCoroutine (CrockettLeave ());
		} else {
			GameObject.Find ("Crockett").GetComponent<NPCRandomMovement> ().Resume ();
		}
	}

	IEnumerator CrockettLeave() {
		yield return StartCoroutine(GameObject.Find("Crockett").GetComponent<NPCController>().MoveUp(10f, 3f));
		Destroy (GameObject.Find ("Crockett"));
	}
}
