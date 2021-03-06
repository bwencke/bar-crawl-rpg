﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : TopLevelController {

	public GoogleAnalyticsV3 googleAnalytics;

	Color selectedColor;
	Color unSelectedColor;
	Color selectedTextColor;
	Color unSelectedTextColor;

	InventoryItemController[] items;
	List<GameObject> createdItemImages = new List<GameObject> ();
	GameObject inventoryGrid;
	public GameObject prefab;

	int selected;
	
	Vector2 prev = Vector2.zero;

	void Start() {
		ColorUtility.TryParseHtmlString ("#0C496CC8", out selectedColor);
		ColorUtility.TryParseHtmlString ("#055F946E", out unSelectedColor);
		ColorUtility.TryParseHtmlString ("#DCE7EEFF", out selectedTextColor);
		ColorUtility.TryParseHtmlString ("#DCE7EEA9", out unSelectedTextColor);

		inventoryGrid = GameObject.FindGameObjectWithTag("InventoryGrid");

		selected = 0;
	}

	public void LoadInventory() {
		googleAnalytics.LogScreen("Inventory");
		items = GameObject.FindGameObjectWithTag ("Player").GetComponentsInChildren<InventoryItemController>();
		foreach (InventoryItemController item in items) {
			if(item.IsEnabled()) {
				GameObject itemImg = Instantiate(prefab) as GameObject;
				Image img = itemImg.GetComponentInChildren<Image>();
				Text text = itemImg.GetComponentInChildren<Text>();
				img.sprite = item.gameObject.GetComponent<SpriteRenderer>().sprite;
				text.text = item.gameObject.name;
				itemImg.transform.SetParent(inventoryGrid.GetComponent<RectTransform>());
				itemImg.transform.localScale = Vector3.one;
				string name = item.gameObject.name;
				itemImg.GetComponent<Button>().onClick.AddListener(() => GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().AccessInventoryItem(name));
				createdItemImages.Add(itemImg);
			}
		}

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		
			// select first item
			createdItemImages[selected].GetComponentInChildren<Text>().color = selectedTextColor;
			createdItemImages[selected].GetComponentInChildren<RawImage>().color = selectedColor;
		
		#endif
	}

	public void ClearInventory() {
		foreach (GameObject itemImage in createdItemImages) {
			Destroy(itemImage);
		}
		createdItemImages.Clear ();
	}

	public override void TriggerMovement(Vector2 movement_vector) {
		//Debug.Log (movement_vector);
		if (movement_vector != prev) {
			if (movement_vector.x != 0 || movement_vector.y != 0) {
				// change color of previous item
				createdItemImages[selected].GetComponentInChildren<Text>().color = unSelectedTextColor;
				createdItemImages[selected].GetComponentInChildren<RawImage>().color = unSelectedColor;

				// change selected item
				int newx = selected % 4;
				int newy = selected / 4;
				if (movement_vector.x != 0) {
					if (0 <= newx + movement_vector.x && newx + movement_vector.x < 4) {
						newx += (int) movement_vector.x;
					}
				}
				else {
					newy -= (int) movement_vector.y;
				}
				int newselected = 4 * newy + newx;
				if (0 <= newselected && newselected < createdItemImages.Count) {
					selected = newselected;
				}
				
				// change color of selected item
				createdItemImages[selected].GetComponentInChildren<Text>().color = selectedTextColor;
				createdItemImages[selected].GetComponentInChildren<RawImage>().color = selectedColor;
			}
			prev = movement_vector;
		}
	}

	public override void TriggerPrimaryAction(System.Action<string> callback) {
		googleAnalytics.LogEvent(new EventHitBuilder()
		                         .SetEventCategory("Inventory")
		                         .SetEventAction("Item Selected"));
		if (callback == null) {
			GameObject.FindGameObjectWithTag ("Alert").GetComponent<AlertController> ().ShowAlert ("What are you trying to do with that?");
		} else {
			callback (createdItemImages [selected].GetComponentInChildren<Text> ().text);
		}
	}
}
