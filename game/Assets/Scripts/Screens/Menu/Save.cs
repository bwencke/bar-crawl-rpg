using UnityEngine;
using System.Collections;

public class Save : MenuItemController {

	public GoogleAnalyticsV3 googleAnalytics;
	public StateController stateController;

	public override void PerformAction() {
		googleAnalytics.LogEvent(new EventHitBuilder()
		                         .SetEventCategory("Menu")
		                         .SetEventAction("Save"));
		Debug.Log ("Saved");
		stateController.SaveState ();
	}
}
