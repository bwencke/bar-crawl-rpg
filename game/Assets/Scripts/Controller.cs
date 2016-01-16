using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CnControls;

public class Controller : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	public GameObject BlackBar1;
	public GameObject BlackBar2;

	public GameObject menu;
	public GameObject inventory;
	public GameObject player;
	public GameObject conversation;
	private GameObject cutscene;
	private GameObject controlling;
	
	private System.Action<string, Object> callback = null;

	// Use this for initialization
	void Start () {
		googleAnalytics.LogEvent(new EventHitBuilder()
		                        .SetEventCategory("Game")
		                        .SetEventAction("Start"));
		SetControlling(player);

		cutscene = null;
		callback = null;

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

	GameObject GetControlling() {
		return controlling;
	}

	void SetControlling(GameObject newController) {
		if (controlling != null) {
			GetControlling ().GetComponent<TopLevelController> ().TriggerMovement (Vector2.zero);
		}
		controlling = newController;
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

		Vector2 input = new Vector2 (horizontal, vertical);

		GetControlling ().GetComponent<TopLevelController> ().TriggerMovement (input);
		if (GetControlling () != player) {
			GameObject.FindGameObjectWithTag("MobileControls").GetComponent<Canvas>().enabled = false;
			player.GetComponent<TopLevelController> ().TriggerMovement (Vector2.zero);
		} else if (GameObject.FindGameObjectWithTag("MobileControls").GetComponent<Canvas>().enabled == false) {
			GameObject.FindGameObjectWithTag("MobileControls").GetComponent<Canvas>().enabled = true;
		}
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
		GetControlling().GetComponent<TopLevelController>().TriggerPrimaryAction(AccessInventoryItem);
	}
	
	void DetectMenuButton() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GetControlling() == inventory) {
				googleAnalytics.LogEvent (new EventHitBuilder ()
				                         .SetEventCategory ("Esc")
				                         .SetEventAction ("Close Inventory"));
				ToggleInventory ();
//			} else if(GetControlling() == conversation) {
//				googleAnalytics.LogEvent (new EventHitBuilder ()
//				                          .SetEventCategory ("Esc")
//				                          .SetEventAction ("Stop Conversation"));
//				StopConversation ();
			} else if(GetControlling() == cutscene || GetControlling() == conversation) {
				return;
			} else {
				googleAnalytics.LogEvent(new EventHitBuilder()
				                         .SetEventCategory("Esc")
				                         .SetEventAction("Close Menu"));
				ToggleMenu ();
			}
		}
	}

	void DetectInventoryButton() {
		if (GetControlling () == player || GetControlling () == inventory) {
			if (Input.GetKeyDown (KeyCode.E)) {
				googleAnalytics.LogEvent (new EventHitBuilder ()
				                         .SetEventCategory ("E")
				                         .SetEventAction (GetControlling () == inventory ? "Close Inventory" : "Open Inventory"));
				ToggleInventory ();
			}
		}
	}

	public void ToggleMenu() {
		menu.GetComponent<Canvas>().enabled = !menu.GetComponent<Canvas>().enabled;
		SetControlling(menu.GetComponent<Canvas>().enabled ? menu : player);
	}

	public void ToggleInventory() {
		if (inventory.GetComponent<Canvas> ().enabled) {
			inventory.GetComponent<InventoryController> ().ClearInventory ();
		} else {
			conversation.GetComponent<ConversationController>().Hide();
			inventory.GetComponent<InventoryController> ().LoadInventory ();
		}
		inventory.GetComponent<Canvas>().enabled = !inventory.GetComponent<Canvas>().enabled;
		if (callback == null) {
			SetControlling (inventory.GetComponent<Canvas> ().enabled ? inventory : player);
		} else {
			SetControlling (inventory.GetComponent<Canvas> ().enabled ? inventory : conversation);
		}
	}

	public void AccessInventory(System.Action<string, Object> callback) {
		ToggleInventory ();
		this.callback = callback;
	}

	public void AccessInventoryItem(string itemName) {
		if (callback != null) {
			Object o = new Option();
			o.name = "true";
			callback (itemName, o);
			if (o.name == "true") {
				ToggleInventory ();
				callback = null;
			}
		} else {
			ToggleInventory ();
		}
	}

	public void StartConversation(string name, string id, System.Action callback) {
		googleAnalytics.LogEvent(new EventHitBuilder()
		                         .SetEventCategory("Conversation")
		                         .SetEventAction("Start"));
		SetControlling(conversation);
		conversation.GetComponent<ConversationController> ().StartConversation (name, id, callback);
	}

	public void StopConversation() {
		googleAnalytics.LogEvent(new EventHitBuilder()
		                         .SetEventCategory("Conversation")
		                         .SetEventAction("Stop"));
		conversation.GetComponent<ConversationController> ().StopConversation ();
		if (cutscene != null) {
			SetControlling(cutscene);
			IncrCutscene();
		} else {
			SetControlling(player);
		}
	}

	public void LoadCutscene(string id) {
		ShowBlackBars ();
		cutscene = GameObject.FindGameObjectWithTag (id + "Cutscene");
		SetControlling(cutscene);
		IncrCutscene ();
	}

	public void LoadCutsceneResults(string id) {
		GameObject.FindGameObjectWithTag (id + "Cutscene").GetComponent<CutsceneScript> ().LoadResults ();
		HideBlackBars();
	}

	private void IncrCutscene() {
		if (cutscene.GetComponent<CutsceneScript> ().HasNext ()) {
			StartCoroutine (cutscene.GetComponent<CutsceneScript> ().Next(IncrCutscene));
			//IncrCutscene();
		} else {
			HideBlackBars();
			cutscene = null;
			SetControlling(player);
		}
	}

	void ShowBlackBars() {
		StartCoroutine (BlackBar1.GetComponent<BlackBarSlider>().SlideIn());
		StartCoroutine (BlackBar2.GetComponent<BlackBarSlider>().SlideIn());
		//BlackBar2.GetComponent<RawImage> ().enabled = true;
	}

	void HideBlackBars() {
		StartCoroutine (BlackBar1.GetComponent<BlackBarSlider>().SlideOut());
		StartCoroutine (BlackBar2.GetComponent<BlackBarSlider>().SlideOut());
		//BlackBar2.GetComponent<RawImage> ().enabled = false;
	}

}
