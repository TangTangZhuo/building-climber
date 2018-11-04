using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFollow : MonoBehaviour {

	Transform camera;

	// Use this for initialization
	void Start () {
		camera = Camera.main.transform;
	//	Time.fixedDeltaTime = 0.02f * 0.35f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, camera.position.y, transform.position.z);
		//transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, camera.position.y, transform.position.z), Time.deltaTime * 2);
	}
}
