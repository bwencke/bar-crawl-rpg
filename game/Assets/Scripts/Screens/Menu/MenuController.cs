using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : TopLevelController {

	Color selectedColor;
	Color unSelectedColor;
	Color selectedTextColor;
	Color unSelectedTextColor;

	GameObject[] options;
	int selected;

	int prev_y = 0;

	// Use this for initialization
	void Start () {
		ColorUtility.TryParseHtmlString ("#0C496CC8", out selectedColor);
		ColorUtility.TryParseHtmlString ("#055F946E", out unSelectedColor);
		ColorUtility.TryParseHtmlString ("#DCE7EEFF", out selectedTextColor);
		ColorUtility.TryParseHtmlString ("#DCE7EEA9", out unSelectedTextColor);

		options = new GameObject[3]{
			GameObject.FindGameObjectWithTag ("MenuItem0"),
			GameObject.FindGameObjectWithTag ("MenuItem1"),
			GameObject.FindGameObjectWithTag ("MenuItem2")
		};
		selected = 0;

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

			// select first item
			options[selected].GetComponentInChildren<Text>().color = selectedTextColor;
			options[selected].GetComponent<Image>().color = selectedColor;

		#endif
	}

	public override void TriggerMovement(Vector2 movement_vector) {
		int y = (int)movement_vector.y;
		if (y != prev_y) {
			if(y != 0) {
				// change color of previous item
				options[selected].GetComponentInChildren<Text>().color = unSelectedTextColor;
				options[selected].GetComponent<Image>().color = unSelectedColor;

				// change selected item
				selected += -1*y;
				if(selected < 0 || selected > (options.Length - 1)) {
					selected -= -1*y;
				}

				// change color of selected item
				options[selected].GetComponentInChildren<Text>().color = selectedTextColor;
				options[selected].GetComponent<Image>().color = selectedColor;
			}
			prev_y = y;
		}
	}

	public override void TriggerPrimaryAction() {
		options [selected].GetComponent<MenuItemController> ().PerformAction ();
	}
}
