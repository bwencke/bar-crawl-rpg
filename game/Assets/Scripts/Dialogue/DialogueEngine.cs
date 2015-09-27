using UnityEngine;
using System.Collections;

public class DialogueEngine : MonoBehaviour
{
	private string level;

	public DialogueEngine(string level) {
		this.level = level;
	}

	public Snippet getSnippet(string name, string guid) {
		return null;
	}
}
