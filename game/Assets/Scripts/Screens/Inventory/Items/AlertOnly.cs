using UnityEngine;
using System.Collections;

public class AlertOnly : InventoryItemController {

	public string msg;

	public override void UseItemOnSelf() {
		GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert(msg);
	}
}
