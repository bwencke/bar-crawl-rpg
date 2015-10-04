using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatementsController : MonoBehaviour {

	public void SetStatement(Statement statement) {
		if (statement == null) {
			return;
		}
		GameObject.FindGameObjectWithTag ("PersonName").GetComponent<Text> ().text = statement.getName () + ":";
		GameObject.FindGameObjectWithTag ("Statement").GetComponent<Text> ().text = statement.getText ();
		gameObject.GetComponent<Canvas> ().enabled = false;
		gameObject.GetComponent<Canvas> ().enabled = true;
	}
}
