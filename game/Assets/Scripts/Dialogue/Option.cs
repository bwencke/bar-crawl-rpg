using UnityEngine;
using System.Collections;

public class Option : ScriptableObject
{
	private string text;
	private string guid;

	public void init(string text, string guid) {
		this.text = text;
		this.guid = guid;
	}

	public string getText() {
		return text;
	}
	
	public string getGUID() {
		return guid;
	}
}
