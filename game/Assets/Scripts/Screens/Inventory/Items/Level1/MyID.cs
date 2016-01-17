using UnityEngine;
using System.Collections;

public class MyID : InventoryItemController {

	public override void UseItemOnSelf() {
		GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert("Sometimes I forget that I'm so handsome.");
	}
}
