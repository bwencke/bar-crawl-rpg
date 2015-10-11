using UnityEngine;
using System.Collections;

public class Snippet : ScriptableObject
{
	private Condition[] assignments;
	private Statement[] statements;
	private Option[] options;

	public void init(Condition[] assignments, Statement[] statements, Option[] options) {
		this.assignments = assignments;
		this.statements = statements;
		this.options = options;
	}

	public Condition[] getAssignments() {
		return assignments;
	}

	public Statement[] getStatements() {
		return statements;
	}

	public Option[] getOptions() {
		return options;
	}
}
