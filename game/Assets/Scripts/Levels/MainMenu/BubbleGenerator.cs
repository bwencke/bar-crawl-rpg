using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BubbleGenerator : MonoBehaviour {

	public GameObject bubblePrefab;

	int count;

	// Use this for initialization
	void Start () {
		count = 0;
		for(int i=0; i<5; i++) {
			GenerateBubble ();
		}
		InvokeRepeating("GenerateBubble", 0, 5);
	}

	void GenerateBubble() {
		//if (count++ < 10) {
			GameObject bubble = Instantiate (bubblePrefab) as GameObject;
			RectTransform bubbleTransform = bubble.GetComponent<RectTransform> ();
			bubbleTransform.SetParent (gameObject.transform);
			bubbleTransform.SetAsFirstSibling ();
			bubbleTransform.localScale = Vector2.one;
			float size = Random.Range (25, 100);
			bubbleTransform.sizeDelta = new Vector2 (size, size);
			bubbleTransform.localPosition = new Vector2 (Random.Range (-350, 350), Random.Range (-300, -200));
			bubbleTransform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, Random.Range (1, 2));
		//}
	}

	public void Destroyed() {
		count--;
	}
}
