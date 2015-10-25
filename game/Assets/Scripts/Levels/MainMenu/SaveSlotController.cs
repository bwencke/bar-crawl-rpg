using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

public class SaveSlotController : MonoBehaviour {

	public int saveSlot;

	public GameObject noSaveData;
	public GameObject progress;
	public GameObject status;

	public Text progressText;
	public Text statusText;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("save_slot" + saveSlot)) {
			noSaveData.SetActive(false);
			progress.SetActive(true);
			status.SetActive(true);
			UpdateData();
		} else {
			noSaveData.SetActive(true);
			progress.SetActive(false);
			status.SetActive(false);
		}
	}

	void UpdateData() {
		string json = PlayerPrefs.GetString ("save_slot" + saveSlot);
		Debug.Log (json);
		JSONNode root = JSON.Parse(json);

		progressText.text = "Bar " + root ["level"] + " (" + Mathf.RoundToInt(root ["level"].AsFloat/7f*100f) + "%)";

		System.DateTime time = System.DateTime.Parse (root ["timestamp"]);
		System.DateTime now = System.DateTime.Now;
		System.TimeSpan difference = now - time;
		if (difference.Days < 1) {
			if(difference.Hours < 1) {
				if(difference.Minutes < 1) {
					statusText.text = "Just now";
				} else {
					statusText.text = difference.Minutes + " minute" + (difference.Minutes == 1 ? "" : "s") + " ago";
				}
			} else {
				statusText.text = difference.Hours + " hour" + (difference.Hours == 1 ? "" : "s") + " ago";
			}
		} else {
			statusText.text = difference.Days + " day" + (difference.Days == 1 ? "" : "s") + " ago";
		}
	}

}
