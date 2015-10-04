using UnityEngine;
using System.Collections;

public class ConversationController : TopLevelController {

	DialogueEngine dialogueEngine;

	string name;
	Snippet snippet;
	public GameObject dialogue;
	public GameObject next;
	public GameObject options;

	Vector2 prev = Vector2.zero;

	int statement;

	void Start() {
		dialogueEngine = new DialogueEngine ("1");
		gameObject.GetComponent<Canvas> ().enabled = true;
		dialogue.GetComponent<Canvas> ().enabled = true;
		next.GetComponent<Canvas> ().enabled = true;
		options.GetComponent<Canvas> ().enabled = true;
		dialogue.GetComponent<Canvas> ().enabled = false;
		next.GetComponent<Canvas> ().enabled = false;
		options.GetComponent<Canvas> ().enabled = false;
		gameObject.GetComponent<Canvas> ().enabled = false;
	}

	public override void TriggerMovement(Vector2 movement_vector) {
		if (movement_vector != prev) {
			if(movement_vector.x > 0 || movement_vector.y < 0) {
				Next ();
			}
			prev = movement_vector;
		}
	}

	public void StartConversation(string name, string id) {
		statement = 0;
		LoadSnippet (id);
		DisplayStatements ();
		gameObject.GetComponent<Canvas> ().enabled = true;
	}

	public void StopConversation() {
		gameObject.GetComponent<Canvas> ().enabled = false;
	}

	void LoadSnippet(string id) {
		snippet = dialogueEngine.getSnippet (name, id);
	}

	void DisplayStatements() {
		if (snippet != null) {
			dialogue.GetComponent<StatementsController> ().SetStatement (snippet.getStatements () [statement]);
		}
		dialogue.GetComponent<Canvas> ().enabled = true;
		next.GetComponent<Canvas> ().enabled = true;
		options.GetComponent<Canvas> ().enabled = false;
	}

	void Next() {
		if (statement < snippet.getStatements ().Length - 1) {
			statement++;
			DisplayStatements();
		} else if(snippet.getOptions() != null && snippet.getOptions().Length > 0) {
			DisplayOptions ();
		} else { 
			StopConversation(); 
		}
	}

	void DisplayOptions() {
		// for each option, add it to the screen
		options.GetComponent<OptionsController> ().SetOptions (snippet.getOptions ());

		dialogue.GetComponent<Canvas> ().enabled = false;
		next.GetComponent<Canvas> ().enabled = false;
		options.GetComponent<Canvas> ().enabled = true;
	}

	public void ChooseOption(string id) {
		LoadSnippet (id);
		DisplayStatements ();
	}
}
