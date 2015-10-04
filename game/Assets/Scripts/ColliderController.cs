using UnityEngine;
using System.Collections;

public class ColliderController : MonoBehaviour {

	public virtual void TriggerPrimaryAction() {
		GameObject.FindGameObjectWithTag("Alert").GetComponent<AlertController>().ShowAlert("You can't do anything with this object!");
	}
}
