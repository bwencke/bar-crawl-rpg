using UnityEngine;
using System.Collections;

public class InventoryItemController : MonoBehaviour {

	public bool enabled = false;

	public bool IsEnabled() {
		return enabled;
	}

	public void Enable() {
		enabled = true;
	}

	public void Disable() {
		enabled = false;
	}

}
