using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float m_speed;
	
	// Update is called once per frame
	void Update () {
	
		if(target) {

			transform.position = Vector3.Lerp (transform.position, target.position, m_speed) + new Vector3(0,0,-10);

		}
	
	}
}
