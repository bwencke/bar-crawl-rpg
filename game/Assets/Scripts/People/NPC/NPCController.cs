using UnityEngine;
using System.Collections;

public class NPCController : ColliderController {

	public GoogleAnalyticsV3 googleAnalytics;

	public string npcName;
	public string conversationId;

	public override void TriggerPrimaryAction() {
		googleAnalytics.LogEvent (new EventHitBuilder ()
		                         .SetEventCategory ("Primary Action")
		                         .SetEventAction ("Talk to NPC")
		                         .SetEventLabel (npcName));
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller>().StartConversation (npcName, conversationId);
	}
}
