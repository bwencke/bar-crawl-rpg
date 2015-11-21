using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

	public Transform warpTarget;
	public GameObject myParent;
	public AudioObject myAudio;
	public GameObject theirParent;
	public AudioObject theirAudio;

	IEnumerator OnTriggerEnter2D(Collider2D collider) {

		ScreenFader sf = GameObject.FindGameObjectWithTag ("Fader").GetComponent<ScreenFader> ();

		yield return StartCoroutine (sf.FadeToBlack());

		Renderer[] renderers = theirParent.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers)
		{
			r.enabled = true;
		}
		Collider2D[] colliders = theirParent.GetComponentsInChildren<Collider2D>();
		foreach (Collider2D c in colliders)
		{
			c.enabled = true;
		}

		renderers = myParent.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers)
		{
			r.enabled = false;
		}
		colliders = myParent.GetComponentsInChildren<Collider2D>();
		foreach (Collider2D c in colliders)
		{
			c.enabled = false;
		}

		//myAudio.StopAudio ();
		//theirAudio.PlayAudio ();

		collider.transform.position = warpTarget.position;
		Camera.main.transform.position = warpTarget.position;

		yield return StartCoroutine(sf.FadeToClear());
	}
}
