using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using SimpleJSON;

public class MainMenuController : MonoBehaviour {

	public Canvas loadingScreen;
	public Canvas saveSlots;

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
		for(int i = 1; i <= 3; i++) {
			if(PlayerPrefs.HasKey("save_slot" + i)) {
				continue;
			}
			loadingScreen.enabled = true;
			PlayerPrefs.SetInt("save_slot", i);
			PlayerPrefs.Save();
			Application.LoadLevel ("Bar1");
		}

		// no open save slots, choose one to overwrite
		//saveSlots.enabled = true;
		loadingScreen.enabled = true;
		PlayerPrefs.SetInt("save_slot", 1);
		PlayerPrefs.DeleteKey ("save_slot1");
		PlayerPrefs.Save();
		Application.LoadLevel ("Bar1");
	}

	void LoadGame() {
		Debug.Log ("load game");
		saveSlots.enabled = true;
	}

	public void LoadGameFromSlot(int save_slot) {
		loadingScreen.enabled = true;
		PlayerPrefs.SetInt("save_slot", save_slot);
		PlayerPrefs.Save();
		if (!PlayerPrefs.HasKey ("save_slot" + save_slot)) {
			Application.LoadLevel ("Bar1");
			return;
		}
		string json = PlayerPrefs.GetString("save_slot"+save_slot);
		JSONNode root = JSON.Parse(json);
		Application.LoadLevel ("Bar"+root ["level"].AsInt);
	}
	
}
