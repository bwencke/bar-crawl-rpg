using UnityEngine;
using System.Collections;

public class NPCController : ColliderController {

	public string npcName;
	public string conversationId;

	public override void TriggerPrimaryAction() {
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller>().StartConversation (npcName, conversationId);
	}
}
