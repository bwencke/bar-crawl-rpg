using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class DialogueEngine : ScriptableObject
{
	private string level;
	private Dictionary<string, bool> vars;

	public void init(string level) {
		this.level = level;
		this.vars = new Dictionary<string, bool>();
	}

	/* Sets a condition variable. */
	public void setVar(string name, bool value) {
		printVars ();
		vars[name] = value;
	}

	/* Checks a condition variable. */
	public bool checkVar(string name) {
		printVars ();
		if (vars.ContainsKey (name)) {
			return vars [name];
		}
		return false;
	}

	public Dictionary<string, bool> getVars() {
		return vars;
	}

	public void printVars() {
		string s = "";
		foreach (KeyValuePair<string, bool> variable in vars)
		{
			s += "Key = {" + variable.Key + "}, Value = {" + variable.Value + "}" + "\n";
		}
		Debug.Log (s);
	}

	/*
	 * Gets a conversation snippet.
	 * 
	 * Comments on the return value:
	 * - Returns null if there is no snippet.
	 * - snippet.assignments, snippet.statements, snippet.options, snippet.options[i].conditions all 
	 *   have a length that could be 0.
	 * - snippet.options[i].id might be "END", which means the conversation should exit.
	 * - snippet.options[i].text might be "JUMP", which means that we should just jump directly to 
	 *   snippet.options[i].id without displaying any options. 
	 */
	public Snippet getSnippet(string name, string guid)
	{
		// Read character file
		TextAsset jsontext = (TextAsset) Resources.Load ("Conversations/" + level + "/" + name, typeof(TextAsset));

		// Parse JSON
		JSONNode root = JSON.Parse (jsontext.ToString());

		// Find snippet
		int snippetno = 0;
		while (true)
		{
			// If no snippet, return null
			if (root["snippets"][snippetno]["id"] == null) {
				Debug.Log ("Dialogue Engine: No snippet.");
				return null;
			}

			// Check if snippet was found
			if (string.Compare (root["snippets"][snippetno]["id"], guid) == 0) {
				break;
			}

			snippetno++;
		}

		// Peform assignments
		int nassignments = 0;
		while (true) {
			if (root["snippets"][snippetno]["assignments"][nassignments] == null) {
				break;
			}
			if (string.Compare (root["snippets"][snippetno]["assignments"][nassignments][1], "FALSE") == 0) {
				this.setVar (root["snippets"][snippetno]["assignments"][nassignments][0], false);
			} else {
				this.setVar (root["snippets"][snippetno]["assignments"][nassignments][0], true);
			}
			nassignments++;
		}

		// Create statements
		int nstatements = 0;
		while (true) {
			if (root["snippets"][snippetno]["statements"][nstatements] == null) {
				break;
			}
			nstatements++;
		}
		Statement[] statements = new Statement[nstatements];
		for (int i = 0; i < nstatements; i++) {
			statements[i] = ScriptableObject.CreateInstance<Statement>();
			statements[i].init (root["snippets"][snippetno]["statements"][i][0], 
			                    root["snippets"][snippetno]["statements"][i][1]);
		}

		// Create options
		int noptions = 0;
		int navailable = 0;
		while (true) {
			if (root["snippets"][snippetno]["options"][noptions] == null) {
				break;
			}
			bool conditionsmet = true;
			int nconditions = 0;
			while (true) {
				if (root["snippets"][snippetno]["options"][noptions]["conditions"][nconditions] == null) {
					break;
				}
				bool val1 = this.checkVar (root["snippets"][snippetno]["options"][noptions]["conditions"][nconditions][0]);
				bool val2 = true;
				if (string.Compare (root["snippets"][snippetno]["options"][noptions]["conditions"][nconditions][1], "FALSE") == 0) {
					val2 = false;
				}
				if (val1 != val2) {
					conditionsmet = false;
				}
				nconditions++;
			}
			if (conditionsmet) {
				navailable++;
			}
			noptions++;
		}
		Option[] options = new Option[navailable];
		noptions = 0;
		navailable = 0;
		while (true) {
			if (root["snippets"][snippetno]["options"][noptions] == null) {
				break;
			}
			bool conditionsmet = true;
			int nconditions = 0;
			while (true) {
				if (root["snippets"][snippetno]["options"][noptions]["conditions"][nconditions] == null) {
					break;
				}
				bool val1 = this.checkVar (root["snippets"][snippetno]["options"][noptions]["conditions"][nconditions][0]);
				bool val2 = true;
				if (string.Compare (root["snippets"][snippetno]["options"][noptions]["conditions"][nconditions][1], "FALSE") == 0) {
					val2 = false;
				}
				if (val1 != val2) {
					conditionsmet = false;
				}
				nconditions++;
			}
			if (conditionsmet) {
				options[navailable] = ScriptableObject.CreateInstance<Option>();
				options[navailable].init (root["snippets"][snippetno]["options"][noptions]["text"],
				                          root["snippets"][snippetno]["options"][noptions]["id"]);
				navailable++;
			}
			noptions++;
		}

		// Create snippet
		Snippet snippet = ScriptableObject.CreateInstance<Snippet>();
		snippet.init (statements, options);
		return snippet;
	}
}
