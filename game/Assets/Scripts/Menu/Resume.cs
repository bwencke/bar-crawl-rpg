using UnityEngine;
using System.Collections;

public class Resume : MenuItemController {
	
	public GameObject controller;

	public override void PerformAction() {
		controller.GetComponent<Controller>().ToggleMenu ();
	}
}
