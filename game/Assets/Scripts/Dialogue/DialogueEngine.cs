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
		JSONNode root = JSON.Parse(jsontext.ToString());
		Debug.Log (root["snippets"][0]["id"]);

		// Stub
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
