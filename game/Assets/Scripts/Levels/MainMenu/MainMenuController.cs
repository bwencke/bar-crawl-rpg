using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public Canvas loadingScreen;

	public Button continueGame;
	public Button newGame;
	public Button loadGame;

	// Use this for initialization
	void Start () {
		continueGame.GetComponent<Button>().onClick.AddListener(() => ContinueGame());
		newGame.GetComponent<Button>().onClick.AddListener(() => NewGame());
		loadGame.GetComponent<Button>().onClick.AddListener(() => LoadGame());
	}

	void ContinueGame() {
		Debug.Log ("continue");
	}

	void NewGame() {
		Debug.Log ("new game");
		loadingScreen.enabled = true;
		Application.LoadLevel ("Bar1");
	}

	void LoadGame() {
		Debug.Log ("load game");
	}
	
}
