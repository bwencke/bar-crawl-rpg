using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

	public Transform warpTarget;
	public GameObject myParent;
	public AudioObject myAudio;
	public GameObject theirParent;
	public AudioObject theirAudio;
	public string locationName;

	IEnumerator OnTriggerEnter2D(Collider2D collider) {

		if (collider.gameObject.name != "Player") {
			return false;
		}

		ScreenFader sf = GameObject.FindGameObjectWithTag ("Fader").GetComponent<ScreenFader> ();
		AlertController ac = GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ();

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

		myAudio.StopAudio ();
		GameObject.FindGameObjectWithTag ("DoorSound").GetComponent<AudioObject> ().PlayAudio();
		theirAudio.PlayAudio ();

		collider.transform.position = new Vector3 (warpTarget.position.x, warpTarget.position.y, collider.transform.position.z);
		Camera.main.transform.position = new Vector3 (warpTarget.position.x, warpTarget.position.y, Camera.main.transform.position.z);

		yield return StartCoroutine(sf.FadeToClear());

		ac.ShowStaticAlert (locationName);
	}
}
