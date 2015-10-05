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
		string jsontext = File.ReadAllText("Assets/Conversations/" + level + "/" + name + ".json");

		// Parse JSON
		JSONNode root = JSON.Parse(jsontext);
		Debug.Log (root["snippets"][0]["id"]);

		// Stub
		Statement[] statements = new Statement[2];
		for (int i = 0; i < statements.Length; i++) {
			statements[i] = ScriptableObject.CreateInstance<Statement>();
		}
		statements[0].init("Frank", "Hello!");
		statements [1].init("Cassidy", "What can I do you for?");
		Option[] options = new Option[2];
		for (int i = 0; i < options.Length; i++) {
			options[i] = ScriptableObject.CreateInstance<Option>();
		}
		options [0].init (null, "$20", "2");
		options [1].init (null, "$100", "3");
		Snippet snippet = ScriptableObject.CreateInstance<Snippet> ();
		snippet.init (null, statements, options);
		return snippet;

		//Statement statement = new Statement ("Frank", "Hello!");
		//Statement statement1 = new Statement("Frank", "How are you doing today?");
		//Statement[] statements = new Statement[]{statement, statement1};
		//Option option = new Option(null, "I'm great. How are you?", "1");
		//Option option1 = new Option(null, "AWFUL. FUCK OFF.", "2");
		//Option[] options = new Option[]{option, option1};
		//Snippet s = new Snippet(null, statements, options);
		//return s;
		return null;
	}
}
