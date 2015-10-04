using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CnControls;

public class Controller : MonoBehaviour {

	public GameObject menu;
	public GameObject inventory;
	public GameObject player;
	public GameObject conversation;
	GameObject controlling;

	// Use this for initialization
	void Start () {
		controlling = player;

		#if UNITY_STANDALONE || UNITY_PLAYER

			GameObject.FindGameObjectWithTag("MobileControls").GetComponent<Canvas>().enabled = false;

		#endif

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
		controlling.GetComponent<TopLevelController>().TriggerPrimaryAction();
	}
	
	void DetectMenuButton() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(controlling == inventory) {
				ToggleInventory();
			} else {
				ToggleMenu ();
			}
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
		if (inventory.GetComponent<Canvas> ().enabled) {
			inventory.GetComponent<InventoryController> ().ClearInventory ();
		} else {
			inventory.GetComponent<InventoryController> ().LoadInventory ();
		}
		inventory.GetComponent<Canvas>().enabled = !inventory.GetComponent<Canvas>().enabled;
		controlling = inventory.GetComponent<Canvas>().enabled ? inventory : player;
	}

	public void StartConversation(string name, string id) {
		controlling = conversation;
		conversation.GetComponent<ConversationController> ().StartConversation (name, id);	
	}

	public void StopConversation() {
		conversation.GetComponent<ConversationController> ().StopConversation ();	
		controlling = player;
	}
}
