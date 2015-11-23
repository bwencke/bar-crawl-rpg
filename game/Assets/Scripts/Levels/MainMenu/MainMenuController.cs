using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using SimpleJSON;

public class MainMenuController : MonoBehaviour {

	public AudioObject music;

	public Canvas loadingScreen;
	public Canvas saveSlots;

	public Button quitGame;
	public Button mute;

	public Button continueGame;
	public Button newGame;
	public Button loadGame;

	public bool overwrite = false;

	// Use this for initialization
	void Start () {
		PlayAudio ();
		quitGame.GetComponent<Button>().onClick.AddListener(() => QuitGame());
		mute.GetComponent<Button>().onClick.AddListener(() => ToggleMute());
		if (PlayerPrefs.HasKey ("save_slot")) {
			continueGame.GetComponent<Button> ().onClick.AddListener (() => ContinueGame ());
		}
		newGame.GetComponent<Button>().onClick.AddListener(() => NewGame());
		loadGame.GetComponent<Button>().onClick.AddListener(() => LoadGame());
	}

	void QuitGame() {
		Application.Quit ();
	}

	void ToggleMute() {
		if (!PlayerPrefs.HasKey ("mute")) {
			PlayerPrefs.SetInt ("mute", 1);
		} else {
			PlayerPrefs.SetInt ("mute", (int)Mathf.Abs(PlayerPrefs.GetInt("mute")-1));
		}
		PlayerPrefs.Save ();

		PlayAudio ();
	}

	void ContinueGame() {
		Debug.Log ("continue");
		LoadGameFromSlot (PlayerPrefs.GetInt ("save_slot"));
	}

	void NewGame() {
		Debug.Log ("new game");
		overwrite = true;
		saveSlots.enabled = true;
	}

	void LoadGame() {
		Debug.Log ("load game");
		overwrite = false;
		saveSlots.enabled = true;
	}

	void PlayAudio() {
		if (!PlayerPrefs.HasKey ("mute")) {
			PlayerPrefs.SetInt("mute", 0);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.GetInt ("mute") == 1) {
			music.StopAudio ();
		} else {
			music.PlayAudio ();
		}
	}

	public void LoadGameFromSlot(int save_slot) {
		loadingScreen.enabled = true;

		// save player's slot
		PlayerPrefs.SetInt("save_slot", save_slot);
		PlayerPrefs.Save();

		// erase save if necessary
		if (overwrite) {
			PlayerPrefs.DeleteKey("save_slot" + save_slot);
		}

		// start new game if empty slot
		if (!PlayerPrefs.HasKey ("save_slot" + save_slot)) {
			Application.LoadLevel ("Bar1");
			return;
		}

		// otherwise restore the game
		string json = PlayerPrefs.GetString ("save_slot" + save_slot);
		JSONNode root = JSON.Parse (json);
		Application.LoadLevel ("Bar" + root ["level"].AsInt);
	}
	
}
