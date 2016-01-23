using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level2Controller : LevelController {

	public override int GetLevel() {
		return 2;
	}

	public override void SetDefaultState() {
		
		GameObject.Find ("StateController").GetComponent<StateController> ().npcImageMap.SetImage ("Cocktail God", "Cocktail God Sunglasses");
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().LoadCutscene("Intro");

	}

	public override void SetState(ConversationController conversationController) {

		GameObject.Find ("StateController").GetComponent<StateController> ().npcImageMap.SetImage ("Cocktail God", "Cocktail God Sunglasses");

		// load variables
		if (conversationController.dialogueEngine.checkVar ("SawIntroCutscene")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().LoadCutsceneResults ("Intro");
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().LoadCutscene ("Intro");
		}

		if (conversationController.dialogueEngine.checkVar ("HasFakeID")) {
			GameObject.Find("Money").GetComponent<InventoryItemController>().Disable();
			GameObject.Find("Fake ID").GetComponent<InventoryItemController>().Enable();
		}

		if (conversationController.dialogueEngine.checkVar ("HasBear")) {
			GameObject.Find("Bear").GetComponent<InventoryItemController>().Enable();
		}

		if (conversationController.dialogueEngine.checkVar ("HasHat")) {
			GameObject.Find("Hat").GetComponent<InventoryItemController>().Enable();
			Destroy(GameObject.Find("Crockett"));
		}

		if (conversationController.dialogueEngine.checkVar ("HasTail")) {
			GameObject.Find("Tail").GetComponent<InventoryItemController>().Enable();
		}

		if (conversationController.dialogueEngine.checkVar ("HasSunglasses")) {
			GameObject.Find("Sunglasses").GetComponent<InventoryItemController>().Enable();
			GameObject.Find ("StateController").GetComponent<StateController> ().npcImageMap.SetImage ("Cocktail God", "Cocktail God");
		}

		if (conversationController.dialogueEngine.checkVar ("WearingMoustache") && conversationController.dialogueEngine.checkVar ("WearingSunglasses")) {
			GameObject.FindGameObjectWithTag("PlayerImage").GetComponent<Image>().sprite = (Sprite) Resources.Load ("Images/BlakeDisguise", typeof(Sprite));
			GameObject.Find("Tail").GetComponent<InventoryItemController>().Disable();
			GameObject.Find("Sunglasses").GetComponent<InventoryItemController>().Disable();
		} else if (conversationController.dialogueEngine.checkVar ("WearingMoustache")) {
			GameObject.FindGameObjectWithTag("PlayerImage").GetComponent<Image>().sprite = (Sprite) Resources.Load ("Images/BlakeMoustache", typeof(Sprite));
			GameObject.Find("Tail").GetComponent<InventoryItemController>().Disable();
		} else if (conversationController.dialogueEngine.checkVar ("WearingSunglasses")) {
			GameObject.FindGameObjectWithTag("PlayerImage").GetComponent<Image>().sprite = (Sprite) Resources.Load ("Images/BlakeSunglasses", typeof(Sprite));
			GameObject.Find("Sunglasses").GetComponent<InventoryItemController>().Disable();
		}

		if (conversationController.dialogueEngine.checkVar ("FakeIDSuccess")) {
			NPCController bouncer = GameObject.FindGameObjectWithTag("Bouncer").GetComponent<NPCController>();
			bouncer.SetPosition (new Vector2 (1.39f, -5.72f));
			bouncer.SetDirection(new Vector2(0, -1));
		}

	}
}
