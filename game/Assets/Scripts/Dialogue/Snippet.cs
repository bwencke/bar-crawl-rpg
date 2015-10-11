using UnityEngine;
using System.Collections;

public class Snippet : ScriptableObject
{
	private Statement[] statements;
	private Option[] options;

	public void init(Statement[] statements, Option[] options) {
		this.statements = statements;
		this.options = options;
	}
	
	public Statement[] getStatements() {
		return statements;
	}

	public Option[] getOptions() {
		return options;
	}
}
