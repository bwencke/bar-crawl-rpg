using UnityEngine;
using System.Collections;

public class BraydenID : InventoryItemController {

	public override void UseItemOnSelf() {
		GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert("I should probably give this to Brayden.");
	}
}
