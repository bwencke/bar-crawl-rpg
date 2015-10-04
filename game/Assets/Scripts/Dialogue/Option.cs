using UnityEngine;
using System.Collections;

public class Option : ScriptableObject
{
	private string[] conditions;
	private string text;
	private string guid;
	
	public Option(string[] conditions, string text, string guid) {
		this.conditions = conditions;
		this.text = text;
		this.guid = guid;
	}

	public string[] getConditions() {
		return conditions;
	}

	public string getText() {
		return text;
	}
	
	public string getGUID() {
		return guid;
	}
}

