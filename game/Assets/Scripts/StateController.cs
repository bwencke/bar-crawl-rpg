using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class StateController : MonoBehaviour {

	int saveSlot;

	// Use this for initialization
	void Start () {
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
	}

	public void SaveState() {

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		Animator playerAnimator = player.GetComponent<Animator> ();

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
				"\"timestamp\":\"" + System.DateTime.Now + "\"" +
			"}";

//		GameObject player = GameObject.FindGameObjectWithTag ("Player");
//		root ["position"] ["x"].AsFloat = player.transform.position.x;
//		root ["position"] ["y"].AsFloat = player.transform.position.y;
//
//		Animator playerAnimator = player.GetComponent<Animator> ();
//		root ["direction"] ["x"].AsFloat = playerAnimator.GetFloat ("input_x");
//		root ["direction"] ["y"].AsFloat = playerAnimator.GetFloat ("input_y");

		PlayerPrefs.SetString ("save_slot" + saveSlot, result);
		//Debug.Log (root.ToString ());
	}
}
