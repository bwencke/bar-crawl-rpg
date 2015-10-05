using UnityEngine;
using System.Collections;

public class Statement : ScriptableObject
{
	private string name;
	private string text;

	public void init(string name, string text) {
		this.name = name;
		this.text = text;
	}

	public string getName() {
		return name;
	}

	public string getText() {
		return text;
	}
}
