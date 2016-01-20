using UnityEngine;
using System.Collections;

public class IntroCutscene : CutsceneScript {

	public NPCController Cassidy;
	public NPCController Brayden;
	public NPCController Josh;

	int position = 0;
	int numScenes = 2;

	int count;

	public override void LoadResults () {
		Brayden.SetPosition(new Vector2(-16.47f, -0.29f));
		Brayden.SetDirection(new Vector2(0, -1));
		Josh.SetPosition(new Vector2 (-33.632f, 0.798f));
		Josh.SetDirection(new Vector2(0, -1));
		Cassidy.SetPosition(new Vector2 (-33.389f, 0.794f));
		Cassidy.SetDirection(new Vector2(0, -1));
	}

	public override IEnumerator Next (System.Action callback) {
		switch(position++) {
		case(0):
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Josh", "1", null);
			break;
		case(1):
			count = 0;
			StartCoroutine(BraydenLeave());
			StartCoroutine(JoshLeave());
			StartCoroutine(CassidyLeave());
			while(count < 3) {
				yield return null;
			}
			LoadResults();
			GameObject.FindGameObjectWithTag ("Conversation").GetComponent<ConversationController> ().dialogueEngine.setVar ("SawIntroCutscene", true);
			callback();
			break;
		}
	}

	public override bool HasNext() {
		return position < numScenes;
	}

	IEnumerator BraydenLeave() {
		yield return StartCoroutine(Brayden.MoveDown(3));
		yield return StartCoroutine(Brayden.MoveRight(1));
		yield return StartCoroutine (Brayden.MoveDown (9));
		count++;
	}

	IEnumerator JoshLeave() {
		yield return StartCoroutine(Josh.MoveLeft(1));
		yield return StartCoroutine(Josh.MoveDown(4));
		yield return StartCoroutine(Josh.MoveRight(1));
		yield return StartCoroutine (Josh.MoveDown (6));
		count++;
	}

	IEnumerator CassidyLeave() {
		yield return StartCoroutine(Cassidy.MoveLeft(2));
		yield return StartCoroutine(Cassidy.MoveDown(5));
		yield return StartCoroutine(Cassidy.MoveRight(0));
		yield return StartCoroutine(Cassidy.MoveDown(5));
		count++;
	}
}
