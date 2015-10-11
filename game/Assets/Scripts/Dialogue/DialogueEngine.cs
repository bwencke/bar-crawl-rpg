using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class DialogueEngine : ScriptableObject
{
	private string level;

	public void init(string level) {
		this.level = level;
	}

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

		// Create assignments
		int nassignments = 0;
		while (true) {
			if (root["snippets"][snippetno]["assignments"][nassignments] == null) {
				break;
			}
			nassignments++;
		}
		Condition[] assignments = new Condition[nassignments];
		for (int i = 0; i < nassignments; i++) {
			assignments[i] = ScriptableObject.CreateInstance<Condition>();
			if (string.Compare (root["snippets"][snippetno]["assignments"][i][1], "FALSE") == 0) {
				assignments[i].init (root["snippets"][snippetno]["assignments"][i][0], false);
			} else {
				assignments[i].init (root["snippets"][snippetno]["assignments"][i][0], true);
			}
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
		while (true) {
			if (root["snippets"][snippetno]["options"][noptions] == null) {
				break;
			}
			noptions++;
		}
		Option[] options = new Option[noptions];
		for (int i = 0; i < noptions; i++) {
			options[i] = ScriptableObject.CreateInstance<Option>();
			int nconditions = 0;
			while (true) {
				if (root["snippets"][snippetno]["options"][i]["conditions"][nconditions] == null) {
					break;
				}
				nconditions++;
			}
			Condition[] conditions = new Condition[nconditions];
			for (int j = 0; j < nconditions; j++) {
				conditions[j] = ScriptableObject.CreateInstance<Condition>();
				if (string.Compare (root["snippets"][snippetno]["options"][i]["conditions"][j][1], "FALSE") == 0) {
					conditions[i].init (root["snippets"][snippetno]["options"][i]["conditions"][j][0], false);
				} else {
					conditions[i].init (root["snippets"][snippetno]["options"][i]["conditions"][j][0], true);
				}
			}
			options[i].init (conditions,
			                 root["snippets"][snippetno]["options"][i]["text"],
			                 root["snippets"][snippetno]["options"][i]["id"]);
		}

		// Create snippet
		Snippet snippet = ScriptableObject.CreateInstance<Snippet>();
		snippet.init (assignments, statements, options);
		return snippet;

		// Stub
		/*
		if (guid == "2") {
			Statement[] statements = new Statement[1];
			for (int i = 0; i < statements.Length; i++) {
				statements [i] = ScriptableObject.CreateInstance<Statement> ();
			}
			statements [0].init ("Frank", "Thank you!");
			Option[] options = new Option[5];
			for (int i = 0; i < options.Length; i++) {
				options [i] = ScriptableObject.CreateInstance<Option> ();
			}
			options [0].init (null, "You're welcome", "4");
			options [1].init (null, "GO FUCK YOURSELF YOU LITTLE BITCH", "5");
			options [2].init (null, "*Punch in Face*", "6");
			options [3].init (null, "Don't mention it!", "7");
			options [4].init (null, "My pleasure!", "8");
			Snippet snippet = ScriptableObject.CreateInstance<Snippet> ();
			snippet.init (null, statements, options);
			return snippet;
		} else {
			Statement[] statements = new Statement[2];
			for (int i = 0; i < statements.Length; i++) {
				statements [i] = ScriptableObject.CreateInstance<Statement> ();
			}
			statements [0].init ("Frank", "Hello!");
			statements [1].init ("Cassidy", "What can I do you for?");
			Option[] options = new Option[2];
			for (int i = 0; i < options.Length; i++) {
				options [i] = ScriptableObject.CreateInstance<Option> ();
			}
			options [0].init (null, "$20", "2");
			options [1].init (null, "$100", "3");
			Snippet snippet = ScriptableObject.CreateInstance<Snippet> ();
			snippet.init (null, statements, options);
			return snippet;
		}
		*/
	}
}
