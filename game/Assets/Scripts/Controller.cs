using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CnControls;

public class Controller : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	public GameObject menu;
	public GameObject inventory;
	public GameObject player;
	public GameObject conversation;
	GameObject controlling;

	// Use this for initialization
	void Start () {
		googleAnalytics.LogEvent(new EventHitBuilder()
		                        .SetEventCategory("Game")
		                        .SetEventAction("Start"));
		controlling = player;

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_PLAYER

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
			googleAnalytics.LogEvent(new EventHitBuilder()
			                         .SetEventCategory("Primary Action")
			                         .SetEventAction(Input.GetKeyDown(KeyCode.Return) ? "Enter Key" : "On Screen Button"));
			TriggerPrimaryAction();
		}
	}

	public void TriggerPrimaryAction() {
		controlling.GetComponent<TopLevelController>().TriggerPrimaryAction();
	}
	
	void DetectMenuButton() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (controlling == inventory) {
				googleAnalytics.LogEvent (new EventHitBuilder ()
				                         .SetEventCategory ("Esc")
				                         .SetEventAction ("Close Inventory"));
				ToggleInventory ();
			} else if(controlling == conversation) {
				googleAnalytics.LogEvent (new EventHitBuilder ()
				                          .SetEventCategory ("Esc")
				                          .SetEventAction ("Stop Conversation"));
				StopConversation ();
			} else {
				googleAnalytics.LogEvent(new EventHitBuilder()
				                         .SetEventCategory("Esc")
				                         .SetEventAction("Close Menu"));
				ToggleMenu ();
			}
		}
	}

	void DetectInventoryButton() {
		if (Input.GetKeyDown (KeyCode.E)) {
			googleAnalytics.LogEvent(new EventHitBuilder()
			                         .SetEventCategory("E")
			                         .SetEventAction(controlling == inventory ? "Close Inventory" : "Open Inventory"));
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
		googleAnalytics.LogEvent(new EventHitBuilder()
		                         .SetEventCategory("Conversation")
		                         .SetEventAction("Start"));
		controlling = conversation;
		conversation.GetComponent<ConversationController> ().StartConversation (name, id);	
	}

	public void StopConversation() {
		googleAnalytics.LogEvent(new EventHitBuilder()
		                         .SetEventCategory("Conversation")
		                         .SetEventAction("Stop"));
		conversation.GetComponent<ConversationController> ().StopConversation ();	
		controlling = player;
	}
}
