using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour
{
	private Condition[] conditions;
	private string text;
	private string guid;
	
	public Option(Condition[] conditions, string text, string guid) {
		this.conditions = conditions;
		this.text = text;
		this.guid = guid;
	}

	public Condition[] getConditions() {
		return conditions;
	}

	public string getText() {
		return text;
	}
	
	public string getGUID() {
		return guid;
	}
}
