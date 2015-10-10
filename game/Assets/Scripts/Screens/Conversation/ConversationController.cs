using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversationController : TopLevelController {

	public GoogleAnalyticsV3 googleAnalytics;

	DialogueEngine dialogueEngine;

	string NPCName;
	Snippet snippet;
	public GameObject dialogue;
	public GameObject next;
	public GameObject options;

	Vector2 prev = Vector2.zero;

	int statement;

	TopLevelController controlling;

	void Start() {
		dialogueEngine = ScriptableObject.CreateInstance<DialogueEngine>();
		dialogueEngine.init("Samples");
	}

	public override void TriggerMovement(Vector2 movement_vector) {
		if (movement_vector != prev) {
			controlling.TriggerMovement (movement_vector);
			prev = movement_vector;
		}
	}

	public override void TriggerPrimaryAction ()
	{
		controlling.TriggerPrimaryAction ();
	}

	public void StartConversation(string name, string id) {
		statement = 0;
		this.NPCName = name;
		LoadSnippet (id);
		gameObject.GetComponent<Canvas> ().enabled = true;
		DisplayStatements ();
	}

	public void StopConversation() {
		gameObject.GetComponent<Canvas> ().enabled = false;
	}

	void LoadSnippet(string id) {
		Debug.Log ("Loading snippet: " + id);
		snippet = dialogueEngine.getSnippet (NPCName, id);
	}

	void DisplayStatements() {
		if (snippet != null) {
			dialogue.GetComponent<StatementsController> ().SetStatement (snippet.getStatements () [statement]);
		}
		dialogue.GetComponent<Canvas> ().enabled = true;
		//next.GetComponent<Canvas> ().enabled = true;
		next.GetComponent<Image> ().enabled = true;
		next.GetComponentInChildren<Text> ().enabled = true;
		options.GetComponent<Canvas> ().enabled = false;
		controlling = dialogue.GetComponent<StatementsController> ();;
	}

	public void Next() {
		if (statement < snippet.getStatements ().Length - 1) {
			statement++;
			DisplayStatements();
		} else if(snippet.getOptions() != null && snippet.getOptions().Length > 0) {
			statement = 0;
			DisplayOptions ();
		} else { 
			statement = 0;
			StopConversation(); 
		}
	}

	void DisplayOptions() {
		// for each option, add it to the screen
		options.GetComponent<OptionsController> ().SetOptions (snippet.getOptions ());

		dialogue.GetComponent<Canvas> ().enabled = false;
		//next.GetComponent<Canvas> ().enabled = false;
		next.GetComponent<Image> ().enabled = false;
		next.GetComponentInChildren<Text> ().enabled = false;
		options.GetComponent<Canvas> ().enabled = true;
		controlling = options.GetComponent<OptionsController> ();
	}

	public void ChooseOption(string id) {
		LoadSnippet (id);
		DisplayStatements ();
	}
}
