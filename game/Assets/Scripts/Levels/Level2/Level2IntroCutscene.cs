using UnityEngine;
using System.Collections;

public class Level2IntroCutscene : CutsceneScript {

	public NPCController Cassidy;
	public NPCController Brayden;
	public NPCController Josh;
	public NPCController Blake;

	int position = 0;
	int numScenes = 2;
	
	int count;

	public override void LoadResults () {
		Brayden.SetPosition(new Vector2(-11.41f, -5.28f));
		Brayden.SetDirection(new Vector2(0, 1));
		Josh.SetPosition(new Vector2 (-11.0f, -4.72f));
		Josh.SetDirection(new Vector2(0, -1));
		Cassidy.SetPosition(new Vector2 (-11.25f, -4.67f));
		Cassidy.SetDirection(new Vector2(0, -1));
		Destroy(Blake);
	}
	
	public override IEnumerator Next (System.Action callback) {
		switch(position++) {
		case(0):
			count = 0;
			StartCoroutine(BraydenLeave());
			StartCoroutine(JoshLeave());
			StartCoroutine(CassidyLeave());
			StartCoroutine(BlakeLeave());
			while(count < 4) {
				yield return null;
			}
			LoadResults();
			GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ().dialogueEngine.setVar ("SawIntroCutscene", true);
			callback();
			break;
		case(1):
			callback();
			Destroy(Blake);
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation("Bouncer", "1", null);
			break;

		}

	}
	
	public override bool HasNext() {
		return position < numScenes;
	}
	
	IEnumerator BraydenLeave() {
		yield return StartCoroutine(Brayden.MoveLeft(10));
		yield return StartCoroutine(Brayden.MoveUp(2));
		count++;
	}
	
	IEnumerator JoshLeave() {
		yield return StartCoroutine(Josh.MoveLeft(9));
		yield return StartCoroutine (Josh.MoveUp (2));
		count++;
	}
	
	IEnumerator CassidyLeave() {
		yield return StartCoroutine(Cassidy.MoveLeft(9));
		yield return StartCoroutine(Cassidy.MoveUp(3));
		count++;
	}

	IEnumerator BlakeLeave() {
		yield return StartCoroutine(Blake.MoveLeft(11.7f));
		yield return StartCoroutine(Blake.MoveUp(0.7f));
		count++;
	}
}
