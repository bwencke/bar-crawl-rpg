using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public GameObject optionPrefab;
	public GameObject optionsContainer;

	public void SetOptions(Option[] options) {
		if (options == null) {
			return;
		}
		foreach (Option option in options) {
			GameObject conversationOption = Instantiate(optionPrefab) as GameObject;
			Text text = conversationOption.GetComponentInChildren<Text>();
			text.text = option.getText();
			conversationOption.transform.SetParent(optionsContainer.GetComponent<RectTransform>());
			conversationOption.transform.localScale = Vector3.one;
		}
	}

	public void ChooseOption(GameObject optionGameObject) {
	}

}
