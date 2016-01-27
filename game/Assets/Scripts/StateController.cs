using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class StateController : MonoBehaviour {

	int saveSlot;
	int level;

	public NPCImageMap npcImageMap;
	public ConversationController conversationController;
	LevelController levelController;

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
			npcImageMap = new NPCImageMap ();
			levelController = GameObject.FindGameObjectWithTag ("level_controller").GetComponent<LevelController> ();
			conversationController.Init (levelController.GetLevel());
			level = levelController.GetLevel();
			GameObject.FindGameObjectWithTag (GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().currentMap).GetComponentsInChildren<AudioObject> () [0].PlayAudio ();
			GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().LoadCutscene("Intro");
			return;
		}
		string json = PlayerPrefs.GetString ("save_slot" + saveSlot);
		JSONNode root = JSON.Parse(json);

		npcImageMap = new NPCImageMap ();
		levelController = GameObject.FindGameObjectWithTag ("level_controller").GetComponent<LevelController> ();
		conversationController.Init (levelController.GetLevel());
		level = root ["level"].AsInt;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if(levelController.GetLevel() == level) {
			player.transform.position = new Vector2(root ["position"]["x"].AsFloat, root ["position"]["y"].AsFloat);
			player.GetComponent<PlayerMovement> ().currentMap = root ["map"];

			Animator playerAnimator = player.GetComponent<Animator> ();
			playerAnimator.SetFloat ("input_x", root ["direction"] ["x"].AsFloat);
			playerAnimator.SetFloat ("input_y", root ["direction"] ["y"].AsFloat);
			foreach (JSONNode variable in root["variables"].AsArray) {
				conversationController.dialogueEngine.setVar(variable["name"], variable["value"].AsBool);
			}

			levelController.SetState (conversationController);
		} else {
			level = levelController.GetLevel();
			levelController.SetDefaultState();
			player.GetComponent<PlayerMovement> ().currentMap = "map_outside";
		}

		GameObject.FindGameObjectWithTag (player.GetComponent<PlayerMovement> ().currentMap).GetComponentsInChildren<AudioObject> () [0].PlayAudio ();
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
					"\"level\":" + levelController.GetLevel() + "," +
					"\"position\":{" +
						"\"x\":" + player.transform.position.x + ","+
						"\"y\":" + player.transform.position.y +
					"}," +
					"\"direction\":{" +
						"\"x\":" + playerAnimator.GetFloat ("input_x") + ","+
						"\"y\":" + playerAnimator.GetFloat ("input_y") +
					"}," +
					"\"map\":\"" + player.GetComponent<PlayerMovement>().currentMap + "\"," +
					"\"timestamp\":\"" + System.DateTime.Now + "\"" + "," +
					"\"variables\":[" +
						variables + 
					"]" +
				"}";

			Debug.Log(result);
			PlayerPrefs.SetString ("save_slot" + saveSlot, result);

			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert ("Successfully saved game.");
		} catch(UnityException e) {
			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert ("Error saving game!");
		}
	}
}
