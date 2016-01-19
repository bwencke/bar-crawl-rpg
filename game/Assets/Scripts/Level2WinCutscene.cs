using UnityEngine;
using System.Collections;

public class Level2WinCutscene : CutsceneScript {

	int position = 0;
	int numScenes = 2;

	public GameObject canvas;
	public GameObject thankYou;
	public AudioObject barAudio;

	public override IEnumerator Next (System.Action callback) {
		switch (position++) {
		case(0):
			barAudio.StopAudio();
			canvas.GetComponent<Canvas> ().enabled = true;
			GameObject.FindGameObjectWithTag("WinParticles").GetComponent<ParticleSystem>().Play();
			canvas.GetComponentInChildren<AudioObject>().PlayAudio();
			yield return new WaitForSeconds(6);
			GameObject.FindGameObjectWithTag("WinParticles").GetComponent<ParticleSystem>().Stop();
			canvas.GetComponent<Canvas> ().enabled = false;
			thankYou.GetComponent<Canvas> ().enabled = true;
			yield return new WaitForSeconds(3);
			Application.LoadLevel ("MainMenu");
			break;
		case(1):
			yield return new WaitForSeconds(10);
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
