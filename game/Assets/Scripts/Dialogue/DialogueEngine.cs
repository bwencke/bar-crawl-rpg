using UnityEngine;
using System.Collections;

public class DialogueEngine : ScriptableObject
{
	public static Snippet getSnippet(string name, string guid) {
		Statement statement = new Statement ("Frank", "Hello!");
		Statement statement1 = new Statement("Frank", "How are you doing today?");
		Statement[] statements = new Statement[]{statement, statement1};
		Option option = new Option(null, "I'm great. How are you?", "1");
		Option option1 = new Option(null, "AWFUL. FUCK OFF.", "2");
		Option[] options = new Option[]{option, option1};
		Snippet s = new Snippet(statements, options);
		return s;
	}
}
