using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlertController : MonoBehaviour {

	bool visible = false;
	bool location = false;
	private IEnumerator coroutine;

	public void ShowAlert(string text) {
		gameObject.GetComponentInChildren<Text> ().text = text;
		visible = true;
		location = false;
		gameObject.GetComponent<Canvas> ().enabled = true;
		coroutine = Wait(2.0f,HideAlert);
		StartCoroutine (coroutine);
	}

	public void ShowLocation(string text) {
		gameObject.GetComponentInChildren<Text> ().text = text;
		visible = true;
		location = true;
		gameObject.GetComponent<Canvas> ().enabled = true;
		coroutine = Wait(2.0f,HideAlert);
		StartCoroutine (coroutine);
	}

	void HideAlert() {
		visible = false;
		gameObject.GetComponent<Canvas> ().enabled = false;
	}

	public void CancelAlert() {
		if (coroutine != null && visible == true && location == false) {
			StopCoroutine (coroutine);
			HideAlert ();
		}
	}

	public IEnumerator Wait(float seconds, System.Action callback) {
		yield return new WaitForSeconds(seconds);
		callback();
	}
}
