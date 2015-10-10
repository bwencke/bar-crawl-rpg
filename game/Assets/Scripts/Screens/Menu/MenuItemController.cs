using UnityEngine;
using System.Collections;

public class MenuItemController : MonoBehaviour {

	public virtual void PerformAction() {
		Debug.Log ("Clicked " + gameObject.name);
	}
}
