using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OptionsController : TopLevelController {

	public ConversationController conversationController;

	public GameObject optionPrefab;
	public GameObject optionsContainer;

	List<GameObject> children = new List<GameObject> ();
	List<string> values = new List<string> ();

	Color selectedColor;
	Color unSelectedColor;
	Color useItemSelectedColor;
	Color useItemUnSelectedColor;

	int selected;

	void Start() {
		ColorUtility.TryParseHtmlString ("#000000FF", out selectedColor);
		ColorUtility.TryParseHtmlString ("#000000A9", out unSelectedColor);
		ColorUtility.TryParseHtmlString ("#2B93D2FF", out useItemSelectedColor);
		ColorUtility.TryParseHtmlString ("#2B93D2A9", out useItemUnSelectedColor);
	}

	public void SetOptions(Option[] options) {
		foreach (GameObject child in children) {
			Destroy(child);
		}
		children.Clear ();
		values.Clear ();
		if (options == null) {
			return;
		}

		GameObject conversationOption;
		Text text;

		foreach (Option option in options) {
			conversationOption = Instantiate(optionPrefab) as GameObject;
			text = conversationOption.GetComponentInChildren<Text>();
			text.text = option.getText();
			conversationOption.transform.SetParent(optionsContainer.GetComponent<RectTransform>());
			conversationOption.transform.localScale = Vector3.one;
			conversationOption.GetComponent<OptionController>().SetId(option.getGUID());
			string id = conversationOption.GetComponent<OptionController>().GetId();
			conversationOption.GetComponent<Button>().onClick.AddListener(() => conversationController.ChooseOption(id));
			children.Add(conversationOption);
			values.Add (option.getGUID());
		}

		// use item
		Option useItemOption = new Option ();
		useItemOption.init ("Use Item", "INVENTORY");
		conversationOption = Instantiate(optionPrefab) as GameObject;
		conversationOption.GetComponent<Image> ().color = useItemUnSelectedColor;
		text = conversationOption.GetComponentInChildren<Text>();
		text.text = useItemOption.getText();
		conversationOption.transform.SetParent(optionsContainer.GetComponent<RectTransform>());
		conversationOption.transform.localScale = Vector3.one;
		conversationOption.GetComponent<OptionController>().SetId(useItemOption.getGUID());
		conversationOption.GetComponent<Button>().onClick.AddListener(() => conversationController.ChooseOption(useItemOption.getGUID()));
		children.Add(conversationOption);
		values.Add (useItemOption.getGUID());

		selected = 0;

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		
		// select first item
		children[selected].GetComponent<Image>().color = selectedColor;
		children[selected].transform.localScale = new Vector3(1.01f, 1.01f);
		
		#endif
	}

	public override void TriggerMovement (Vector2 movement_vector)
	{
		int y = (int)movement_vector.y;
		if(y != 0) {
			// change color of previous item
			children[selected].GetComponent<Image>().color = selected == (children.Count-1) ? useItemUnSelectedColor : unSelectedColor;
			children[selected].transform.localScale = new Vector3(1, 1);
	
			// change selected item
			selected += -1*y;
			if(selected < 0 || selected > (children.Count - 1)) {
				selected -= -1*y;
			}
	
	 		// change color of selected item
			children[selected].GetComponent<Image>().color = selected == (children.Count-1) ? useItemSelectedColor : selectedColor;
			children[selected].transform.localScale = new Vector3(1.01f, 1.01f);
		}
	}

	public override void TriggerPrimaryAction (System.Action<string> callback)
	{
		conversationController.ChooseOption(values[selected]);
	}

}
