using UnityEngine;
using System.Collections;

public class rotation : MonoBehaviour {

	public int direction = 1;

	// Use this for initialization
	void Update () {
		transform.Rotate (new Vector3 (0, 0, direction*.5f));
	}

}
