using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using SimpleJSON;

public class MainMenuController : MonoBehaviour {

	public Canvas loadingScreen;

	public Button continueGame;
	public Button newGame;
	public Button loadGame;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("save_slot")) {
			continueGame.GetComponent<Button> ().onClick.AddListener (() => ContinueGame ());
		}
		newGame.GetComponent<Button>().onClick.AddListener(() => NewGame());
		loadGame.GetComponent<Button>().onClick.AddListener(() => LoadGame());
	}

	void ContinueGame() {
		Debug.Log ("continue");
		LoadGameFromSlot (PlayerPrefs.GetInt ("save_slot"));
	}

	void NewGame() {
		Debug.Log ("new game");
		loadingScreen.enabled = true;
		Application.LoadLevel ("Bar1");
	}

	void LoadGame() {
		Debug.Log ("load game");
		loadingScreen.enabled = true;
		LoadGameFromSlot (1);
	}

	void LoadGameFromSlot(int save_slot) {
		string json = PlayerPrefs.GetString("save_slot"+save_slot);
		JSONNode root = JSON.Parse(json);
		PlayerPrefs.SetInt ("save_slot", save_slot);
		PlayerPrefs.Save ();
		Application.LoadLevel ("Bar"+root ["level"].AsInt);
	}
	
}
