using UnityEngine;
using System.Collections;

public class Snippet : MonoBehaviour
{
	private Statement[] statements;
	private Option[] options;

	public Snippet(Statement[] statements, Option[] options) {
		this.statements = statements;
		this.options = options;
	}

	public Statement[] getStatements() {
		return statements;
	}

	public Statement[] getOptions() {
		return options;
	}
}
