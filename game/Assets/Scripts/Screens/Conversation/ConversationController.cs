using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversationController : TopLevelController {

	public Controller controller;

	public GoogleAnalyticsV3 googleAnalytics;

	public DialogueEngine dialogueEngine;

	string NPCName;
	Snippet snippet;
	public GameObject npcImage;
	public GameObject playerImage;
	public GameObject dialogue;
	public GameObject next;
	public GameObject options;

	private System.Action callback;

	Vector2 prev = Vector2.zero;

	int statement;

	TopLevelController controlling;

	public void Init(int level) {
		dialogueEngine = ScriptableObject.CreateInstance<DialogueEngine>();
		dialogueEngine.init("Level"+level);
	}

	public override void TriggerMovement(Vector2 movement_vector) {
		if (movement_vector != prev) {
			controlling.TriggerMovement (movement_vector);
			prev = movement_vector;
		}
	}

	public override void TriggerPrimaryAction (System.Action<string> callback)
	{
		controlling.TriggerPrimaryAction (callback);
	}

	public void StartConversation(string name, string id, System.Action callback) {
		this.callback = callback;
		this.NPCName = name;
		LoadSnippet (id);
		gameObject.GetComponent<Canvas> ().enabled = true;
		Next();
	}

	public void StopConversation() {
		gameObject.GetComponent<Canvas> ().enabled = false;
		npcImage.GetComponent<Canvas> ().enabled = false;
		playerImage.GetComponent<Canvas> ().enabled = false;
		dialogue.GetComponent<Canvas> ().enabled = false;
		next.GetComponent<Image> ().enabled = false;
		next.GetComponentInChildren<Text> ().enabled = false;
		Hide ();
		if (callback != null) {
			callback ();
		}
	}

	public void Hide() {
		options.GetComponent<Canvas> ().enabled = false;
	}

	void LoadSnippet(string id) {
		snippet = dialogueEngine.getSnippet (NPCName, id);
		statement = -1;
	}

	void DisplayStatements() {
		//if (snippet == null && snippet.getStatements () != null && snippet.getStatements().Length > 0) {
			Debug.Log (statement);
			dialogue.GetComponent<StatementsController> ().SetStatement (snippet.getStatements () [statement]);
		//}
		dialogue.GetComponent<Canvas> ().enabled = true;
		next.GetComponent<Image> ().enabled = true;
		next.GetComponentInChildren<Text> ().enabled = true;
		options.GetComponent<Canvas> ().enabled = false;
		controlling = dialogue.GetComponent<StatementsController> ();
	}

	public void Next() {
		if (snippet != null && snippet.getStatements () != null && statement < snippet.getStatements ().Length - 1) {
			statement++;
			DisplayStatements();
		} else if(snippet != null && snippet.getOptions() != null && snippet.getOptions().Length > 0) {
			DisplayOptions ();
		} else { 
			controller.StopConversation(); 
		}
	}

	void DisplayOptions() {
		if (snippet.getOptions ().Length == 1 && snippet.getOptions () [0].getText () == "JUMP") {
			ChooseOption(snippet.getOptions () [0].getGUID ());
			return;
		}

		// for each option, add it to the screen
		options.GetComponent<OptionsController> ().SetOptions (snippet.getOptions ());

		dialogue.GetComponent<Canvas> ().enabled = false;
		npcImage.GetComponent<Canvas> ().enabled = false;
		playerImage.GetComponent<Canvas> ().enabled = false;
		next.GetComponent<Image> ().enabled = false;
		next.GetComponentInChildren<Text> ().enabled = false;
		options.GetComponent<Canvas> ().enabled = true;
		controlling = options.GetComponent<OptionsController> ();
	}

	public void ChooseOption(string id) {
		ChooseOptionO (id, null);
	}

	public void ChooseOptionO(string id, Object o) {
		//Debug.LogError ("OPTION = " + id);
		if (id == "END") {
			controller.StopConversation ();
			return;
		} else if (id == "Inventory") {
			controller.AccessInventory(ChooseOptionO);
			return;
		}
		if (dialogueEngine.getSnippet (NPCName, id) == null) {
			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert("I don't think I can do that!");
			if(o != null) {
				o.name = "false";
			}
		} else {
			LoadSnippet (id);
			Next ();
		}
	}

}
