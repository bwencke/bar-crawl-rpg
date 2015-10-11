using UnityEngine;
using System.Collections;

public class Quit : MenuItemController {

	public GoogleAnalyticsV3 googleAnalytics;

	public override void PerformAction() {
		googleAnalytics.LogEvent(new EventHitBuilder()
		                         .SetEventCategory("Menu")
		                         .SetEventAction("Quit"));
		Application.LoadLevel ("MainMenu");
	}
}
