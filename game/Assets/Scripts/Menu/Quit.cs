using UnityEngine;
using System.Collections;

public class Quit : MenuItemController {

	public override void PerformAction() {
		Debug.Log ("Quitting...");
		Application.Quit ();
	}
}
