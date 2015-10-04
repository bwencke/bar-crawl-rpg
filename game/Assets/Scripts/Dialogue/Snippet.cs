using UnityEngine;
using System.Collections;

public class Snippet : ScriptableObject
{
	private Condition[] conditions;
	private Statement[] statements;
	private Option[] options;

	public Snippet(Condition[] conditions, Statement[] statements, Option[] options) {
		this.conditions = conditions;
		this.statements = statements;
		this.options = options;
	}

	public Condition[] getConditions() {
		return conditions;
	}

	public Statement[] getStatements() {
		return statements;
	}

	public Option[] getOptions() {
		return options;
	}
}
