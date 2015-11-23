using UnityEngine;
using System.Collections;

public class WinCutscene : CutsceneScript {

	int position = 0;
	int numScenes = 1;

	public GameObject canvas;

	public override IEnumerator Next (System.Action callback) {
		switch (position++) {
		case(0):
			canvas.GetComponent<Canvas> ().enabled = true;
			break;
		}
		yield return null;
	}

	public override bool HasNext() {
		return position < numScenes;
	}

	public override void LoadResults () {
	}
}
