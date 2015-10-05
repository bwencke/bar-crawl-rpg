using UnityEngine;
using System.Collections;

public class Condition : ScriptableObject
{
	private string name;
	private bool value;

	public void init(string name, bool value) {
		this.name = name;
		this.value = value;
	}

	public string getName() {
		return name;
	}

	public bool getValue() {
		return value;
	}
}
