using UnityEngine;
using System.Collections;

public class BankerDesk : ColliderController {
	public override void TriggerPrimaryAction () {
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation("Banker", "1", null);
	}
}
