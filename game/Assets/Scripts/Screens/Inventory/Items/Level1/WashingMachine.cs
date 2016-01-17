using UnityEngine;
using System.Collections;

public class WashingMachine : InventoryItemController {

	public override void UseItemOnSelf() {
		GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert("How does this even fit in my bag?");
	}
}
