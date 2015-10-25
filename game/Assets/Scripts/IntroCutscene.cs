using UnityEngine;
using System.Collections;

public class IntroCutscene : CutsceneScript {

	public NPCController Cassidy;
	public NPCController Brayden;
	public NPCController Josh;

	int position = 0;
	int numScenes = 2;

	public override IEnumerator Next (System.Action callback) {
		Debug.LogError (position);
		switch(position++) {
		case(0):
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller> ().StartConversation ("Josh", "1");
			break;
		case(1):
			StartCoroutine(BraydenLeave());
			StartCoroutine(JoshLeave());
			yield return StartCoroutine(CassidyLeave());
			Brayden.SetPosition(new Vector2(10.38f, -8.85f));
			Brayden.SetDirection(new Vector2(0, -1));
			Destroy(Josh.gameObject);
			Destroy(Cassidy.gameObject);
			callback();
			break;
		}
	}

	public override bool HasNext() {
		return !(position >= numScenes);
	}

	IEnumerator BraydenLeave() {
		yield return StartCoroutine(Brayden.MoveDown(4));
		yield return StartCoroutine(Brayden.MoveRight(1));
		yield return StartCoroutine (Brayden.MoveDown (6));
	}

	IEnumerator JoshLeave() {
		yield return StartCoroutine(Josh.MoveLeft(1));
		yield return StartCoroutine(Josh.MoveDown(4));
		yield return StartCoroutine(Josh.MoveRight(1));
		yield return StartCoroutine (Josh.MoveDown (6));
	}

	IEnumerator CassidyLeave() {
		yield return StartCoroutine(Cassidy.MoveLeft(2));
		yield return StartCoroutine(Cassidy.MoveDown(4));
		yield return StartCoroutine(Cassidy.MoveRight(1));
		yield return StartCoroutine(Cassidy.MoveDown(6));
	}
}
