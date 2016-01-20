using UnityEngine;
using System.Collections;

public class WinCutscene : CutsceneScript {

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
			Application.LoadLevel ("Bar2");
			break;
		case(1):
			yield return new WaitForSeconds(2);
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
