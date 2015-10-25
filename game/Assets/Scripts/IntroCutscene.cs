using UnityEngine;
using System.Collections;

public class IntroCutscene : CutsceneScript {

	public override void Begin () {
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Josh", "1");
	}
}
