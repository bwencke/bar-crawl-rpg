using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float m_speed;
	Camera myCam;


	// Use this for initialization
	void Start () {

		myCam = GetComponent<Camera> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		//myCam.orthographicSize = (Screen.height / 100f) / 8f;

		if(target) {

			transform.position = Vector3.Lerp (transform.position, target.position, m_speed) + new Vector3(0,0,-10);

		}
	
	}
}
