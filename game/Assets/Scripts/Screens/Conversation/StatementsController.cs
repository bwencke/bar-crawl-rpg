using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatementsController : TopLevelController {

	public GameObject npcImage;
	public GameObject playerImage;

	public ConversationController conversationController;

	public void SetStatement(Statement statement) {
		if (statement == null) {
			return;
		}
		GameObject.FindGameObjectWithTag ("PersonName").GetComponent<Text> ().text = statement.getName () + ":";
		GameObject.FindGameObjectWithTag ("Statement").GetComponent<Text> ().text = statement.getText ();
		if (statement.getName () == "Blake") {
			npcImage.GetComponent<Canvas> ().enabled = false;
			playerImage.GetComponent<Canvas> ().enabled = true;
		} else {
			npcImage.GetComponent<Canvas> ().enabled = true;
			playerImage.GetComponent<Canvas> ().enabled = false;
		}
		gameObject.GetComponent<Canvas> ().enabled = false;
		gameObject.GetComponent<Canvas> ().enabled = true;
	}

	public override void TriggerMovement (Vector2 movement_vector)
	{
		if (movement_vector.x > 0 || movement_vector.y < 0) {
			conversationController.googleAnalytics.LogEvent(new EventHitBuilder()
			                        .SetEventCategory("Conversation")
			                         .SetEventAction(movement_vector.x > 0 ? "Next Using RIGHT ARROW" : "Next Using DOWN ARROW"));
			conversationController.Next ();
		}
		conversationController.googleAnalytics.LogEvent(new EventHitBuilder()
		                         .SetEventCategory("Conversation")
		                         .SetEventAction("KEYS: ( " + movement_vector.x + ", " + movement_vector.y + " )"));
	}

	public override void TriggerPrimaryAction ()
	{
		conversationController.Next ();
	}
}
