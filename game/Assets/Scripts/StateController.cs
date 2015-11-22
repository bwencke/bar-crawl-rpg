using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class StateController : MonoBehaviour {

	int saveSlot;

	public ConversationController conversationController;

	// Use this for initialization
	public void Begin () {
		// default to save slot 1
		if (!PlayerPrefs.HasKey ("save_slot")) {
			PlayerPrefs.SetInt("save_slot", 1);
			PlayerPrefs.Save();
		}

		// load saved data
		saveSlot = PlayerPrefs.GetInt ("save_slot");
		LoadDataFromSlot ();
	}
	
	void LoadDataFromSlot() {
		if (!PlayerPrefs.HasKey ("save_slot" + saveSlot)) {
			GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().LoadCutscene("Intro");
			return;
		}
		string json = PlayerPrefs.GetString ("save_slot" + saveSlot);
		JSONNode root = JSON.Parse(json);

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		player.transform.position = new Vector2(root ["position"]["x"].AsFloat, root ["position"]["y"].AsFloat);

		Animator playerAnimator = player.GetComponent<Animator> ();
		playerAnimator.SetFloat ("input_x", root ["direction"] ["x"].AsFloat);
		playerAnimator.SetFloat ("input_y", root ["direction"] ["y"].AsFloat);
		foreach (JSONNode variable in root["variables"].AsArray) {
			conversationController.dialogueEngine.setVar(variable["name"], variable["value"].AsBool);
		}
		if (conversationController.dialogueEngine.checkVar ("SawIntroCutscene")) {
			GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().LoadCutsceneResults("Intro");
		}
		if (conversationController.dialogueEngine.checkVar ("FoundID")) {
			GameObject.FindGameObjectWithTag("BraydensID").GetComponent<InventoryItemController>().Enable();
			GameObject.FindGameObjectWithTag("WashingMachine").GetComponent<InventoryItemController>().Enable();
			Destroy(GameObject.FindGameObjectWithTag("THEWasher"));
		}
		conversationController.dialogueEngine.printVars ();
	}

	public void SaveState() {
		try {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			Animator playerAnimator = player.GetComponent<Animator> ();

			string variables = "";
			foreach (KeyValuePair<string, bool> variable in conversationController.dialogueEngine.getVars())
			{
				variables += "{" + 
						"\"name\":\"" + variable.Key + "\"," +
						"\"value\":" + variable.Value.ToString() + 
					"},";
			}
			if (variables.Length > 0) {
				variables = variables.Remove (variables.Length - 1);
			}

			string result = "{" +
					"\"level\":1," +
					"\"position\":{" +
						"\"x\":" + player.transform.position.x + ","+
						"\"y\":" + player.transform.position.y +
					"}," +
					"\"direction\":{" +
						"\"x\":" + playerAnimator.GetFloat ("input_x") + ","+
						"\"y\":" + playerAnimator.GetFloat ("input_y") +
					"}," +
					"\"timestamp\":\"" + System.DateTime.Now + "\"" + "," +
					"\"variables\":[" +
						variables + 
					"]" +
				"}";

	//		GameObject player = GameObject.FindGameObjectWithTag ("Player");
	//		root ["position"] ["x"].AsFloat = player.transform.position.x;
	//		root ["position"] ["y"].AsFloat = player.transform.position.y;
	//
	//		Animator playerAnimator = player.GetComponent<Animator> ();
	//		root ["direction"] ["x"].AsFloat = playerAnimator.GetFloat ("input_x");
	//		root ["direction"] ["y"].AsFloat = playerAnimator.GetFloat ("input_y");

			PlayerPrefs.SetString ("save_slot" + saveSlot, result);

			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert ("Successfully saved game.");
		} catch(UnityException e) {
			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert ("Error saving game!");
		}
	}
}
