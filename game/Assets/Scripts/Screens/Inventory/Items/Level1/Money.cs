using UnityEngine;
using System.Collections;

public class Money : InventoryItemController {

	public override void UseItemOnSelf() {
		GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert("Only $199,995 short of a Lamborghini.");
	}
}
