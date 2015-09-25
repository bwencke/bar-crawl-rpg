using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioObject : MonoBehaviour {

	public void PlayAudio() {
		GetComponent<AudioSource>().Play();
	}

	public void StopAudio() {
		GetComponent<AudioSource> ().Stop ();
	}
}
