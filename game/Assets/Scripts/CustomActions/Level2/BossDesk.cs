using UnityEngine;
using System.Collections;

public class BossDesk : ColliderController {
	public override void TriggerPrimaryAction () {
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Boss", "1", null);
	}
}
