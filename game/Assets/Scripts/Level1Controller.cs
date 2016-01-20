using UnityEngine;
using System.Collections;

public class Level1Controller : LevelController {

	public override int GetLevel() {
		return 1;
	}

	public override void SetDefaultState() {
	}

	public override void SetState(ConversationController conversationController) {
		GameObject.FindGameObjectWithTag ("Banker").GetComponent<NPCController> ().SetDirection (new Vector2 (1, 0));
		
		// load variables
		if (conversationController.dialogueEngine.checkVar ("SawIntroCutscene")) {
			GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().LoadCutsceneResults("Intro");
		}
		if (conversationController.dialogueEngine.checkVar ("FoundID")) {
			GameObject.FindGameObjectWithTag("BraydensID").GetComponent<InventoryItemController>().Enable();
			GameObject.FindGameObjectWithTag("WashingMachine").GetComponent<InventoryItemController>().Enable();
			Destroy(GameObject.FindGameObjectWithTag("THEWasher"));
		}
		if (conversationController.dialogueEngine.checkVar ("BraydenIDReturned")) {
			GameObject.FindGameObjectWithTag ("BraydensID").GetComponent<InventoryItemController> ().Disable ();
			NPCController brayden = GameObject.FindGameObjectWithTag("Brayden").GetComponent<NPCController>();
			brayden.SetPosition (new Vector2 (-33.117f, 0.657f));
			brayden.SetDirection(new Vector2(-1, 0));
		}
		if (conversationController.dialogueEngine.checkVar ("ShowedID")) {
			NPCController bouncer = GameObject.FindGameObjectWithTag("Bouncer").GetComponent<NPCController>();
			bouncer.SetPosition (new Vector2 (-17.62f, -0.05f));
			bouncer.SetDirection(new Vector2(0, -1));
		}
		if (conversationController.dialogueEngine.checkVar ("HasMoney")) {
			GameObject.FindGameObjectWithTag ("Money").GetComponent<InventoryItemController> ().Enable ();
		}
	}
}
