using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCImageMap : MonoBehaviour {

	public Dictionary<string, string> imgs;

	public NPCImageMap() {
		imgs = new Dictionary<string, string>();
	}

	public string GetImage(string npcName) {
		string img = null;
		imgs.TryGetValue (npcName, out img);
		if (img != null) {
			return img;
		} else {
			return npcName;
		}
	}

	public void SetImage(string npcName, string imgFile) {
		imgs [npcName] = imgFile;
	}

}
