using UnityEngine;
using System.Collections;

public class BartenderCounter : ColliderController {
	public override void TriggerPrimaryAction () {
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation("Bartender", "1", null);
	}
}
