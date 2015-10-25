using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionController : MonoBehaviour {

	string id;

	// Use this for initialization
	public void SetId(string id) {
		this.id = id;
	}

	public string GetId() {
		return id;
	}
}
