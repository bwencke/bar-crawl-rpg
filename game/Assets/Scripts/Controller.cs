using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CnControls;

public class Controller : MonoBehaviour {

	public GameObject menu;
	public GameObject inventory;
	public GameObject player;
	GameObject controlling;

	// Use this for initialization
	void Start () {
		controlling = player;
	}

	// Update is called once per frame
	void Update () {
		DetectArrowKeys ();
		DetectPrimaryAction ();
		DetectMenuButton ();
		DetectInventoryButton ();
	}

	void DetectArrowKeys() {

		int horizontal = 0;
		int vertical = 0;

		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		
			horizontal = (int) Input.GetAxisRaw ("Horizontal");
			vertical = (int) Input.GetAxisRaw ("Vertical");
		
		#else 
		
			horizontal = (int) CnInputManager.GetAxisRaw("Horizontal");
			vertical = (int) CnInputManager.GetAxisRaw("Vertical");
		
		#endif

		controlling.GetComponent<TopLevelController>().TriggerMovement(new Vector2 (horizontal, vertical));

	}

	void DetectPrimaryAction() {
		if (CnInputManager.GetButtonDown ("Jump") || Input.GetKeyDown(KeyCode.Return)) {
			TriggerPrimaryAction();
		}
	}

	public void TriggerPrimaryAction() {
		GameObject.FindGameObjectWithTag ("MenuButton").GetComponent<Image> ().enabled = !GameObject.FindGameObjectWithTag ("MenuButton").GetComponent<Image> ().enabled;

		controlling.GetComponent<TopLevelController>().TriggerPrimaryAction();
	}
	
	void DetectMenuButton() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			ToggleMenu ();
		}
	}

	void DetectInventoryButton() {
		if (Input.GetKeyDown (KeyCode.E)) {
			ToggleInventory();
		}
	}

	public void ToggleMenu() {
		menu.GetComponent<Canvas>().enabled = !menu.GetComponent<Canvas>().enabled;
		controlling = menu.GetComponent<Canvas>().enabled ? menu : player;
	}

	public void ToggleInventory() {
		inventory.GetComponent<Canvas>().enabled = !inventory.GetComponent<Canvas>().enabled;
		controlling = inventory.GetComponent<Canvas>().enabled ? inventory : player;
	}
}
