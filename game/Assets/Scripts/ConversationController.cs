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

	GameObject controlling;

	void Start() {
		dialogueEngine = ScriptableObject.CreateInstance<DialogueEngine>();
		dialogueEngine.init("Samples");
	}

	public override void TriggerMovement(Vector2 movement_vector) {
		if (movement_vector != prev) {
			if (controlling == dialogue) {
				if (movement_vector.x > 0 || movement_vector.y < 0) {
					Next ();
				}
			} else {
//				if (movement_vector.x > 0 || movement_vector.y < 0) {
//					Next ();
//				}
				Debug.Log ("moving through options");
				int y = (int)movement_vector.y;
				if(y != 0) {
//					// change color of previous item
//					options[selected].GetComponentInChildren<Text>().color = unSelectedTextColor;
//					options[selected].GetComponent<Image>().color = unSelectedColor;
					
//					// change selected item
//					selected += -1*y;
//					if(selected < 0 || selected > (options.Length - 1)) {
//						selected -= -1*y;
//					}
					
					// change color of selected item
//					options[selected].GetComponentInChildren<Text>().color = selectedTextColor;
//					options[selected].GetComponent<Image>().color = selectedColor;
				}
			}
			prev = movement_vector;
		}
	}

	public void StartConversation(string name, string id) {
		statement = 0;
		this.name = name;
		LoadSnippet (id);
		gameObject.GetComponent<Canvas> ().enabled = true;
		DisplayStatements ();
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
		controlling = dialogue;
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
		controlling = options;
	}

	public void ChooseOption(string id) {
		LoadSnippet (id);
		DisplayStatements ();
	}
}
