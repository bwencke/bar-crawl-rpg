using UnityEngine;
using System.Collections;

public class TopLevelController : MonoBehaviour {

	public virtual void TriggerMovement(Vector2 movement_vector) {
		if (movement_vector != Vector2.zero) {
			Debug.Log (movement_vector);
		}
	}

	public virtual void TriggerPrimaryAction(System.Action<string> callback) {
		Debug.Log ("Primary Action Triggered");
	}

	public virtual void KeyDown() {
		Debug.Log ("clicked");
	}
}
