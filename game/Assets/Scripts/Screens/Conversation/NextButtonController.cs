using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NextButtonController : MonoBehaviour {

	public ConversationController conversationController;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Button> ().onClick.AddListener(() => {
			Debug.Log ("hello!");
			conversationController.Next();
		});
	}
	
	void Next() {
		Debug.Log ("hello!");
		conversationController.Next();
	}
}
