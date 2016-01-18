using UnityEngine;
using System.Collections;

public class Level2Controller : LevelController {

	public override int GetLevel() {
		return 2;
	}

	public override void SetDefaultState() {

		GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().LoadCutscene("Intro");

	}

	public override void SetState(ConversationController conversationController) {

		// load variables
		if (conversationController.dialogueEngine.checkVar ("SawIntroCutscene")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().LoadCutsceneResults ("Intro");
		} else {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().LoadCutscene ("Intro");
		}

	}
}
