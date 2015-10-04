using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OptionsController : MonoBehaviour {

	public GameObject optionPrefab;
	public GameObject optionsContainer;

	List<GameObject> children = new List<GameObject> ();

	public void SetOptions(Option[] options) {
		foreach (GameObject child in children) {
			Destroy(child);
		}
		children.Clear ();
		if (options == null) {
			return;
		}
		foreach (Option option in options) {
			GameObject conversationOption = Instantiate(optionPrefab) as GameObject;
			Text text = conversationOption.GetComponentInChildren<Text>();
			text.text = option.getText();
			conversationOption.transform.SetParent(optionsContainer.GetComponent<RectTransform>());
			conversationOption.transform.localScale = Vector3.one;
			children.Add(conversationOption);
		}
	}

	public void ChooseOption(GameObject optionGameObject) {
	}

}
