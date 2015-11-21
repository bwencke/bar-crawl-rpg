using UnityEngine;
using System.Collections;

public class BoundsCollider : ColliderController {

	public string msg;
	public string var;

	public override void TriggerPrimaryAction () {
		if (msg.Length > 0) {
			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert (msg);
		}

		if (var.Length > 0) {
			SetVar ();
		}
	}

	private void SetVar() {
		GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ().dialogueEngine.setVar (var, true);
	}

}
