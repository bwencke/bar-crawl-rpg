using UnityEngine;
using System.Collections;

public class NPCController : ColliderController {

	public GoogleAnalyticsV3 googleAnalytics;

	public string npcName;
	public string conversationId;

	void Start() {
		gameObject.GetComponent<Animator>().SetFloat("input_x", 0);
		gameObject.GetComponent<Animator>().SetFloat("input_y", -1);
		gameObject.GetComponent<Animator>().SetBool("is_walking", false);
	}

	public override void TriggerPrimaryAction() {
		googleAnalytics.LogEvent (new EventHitBuilder ()
		                         .SetEventCategory ("Primary Action")
		                         .SetEventAction ("Talk to NPC")
		                         .SetEventLabel (npcName));
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller>().StartConversation (npcName, conversationId);
	}

	public void Move(Vector2 movement_vector) {
	}
}
