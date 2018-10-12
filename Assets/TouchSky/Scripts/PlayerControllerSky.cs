using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSky : MonoBehaviour {
	public float swingForce = 500;
	public Transform rocket;
	Rigidbody2D rig2d;
	// Use this for initialization
	void Start () {
		rig2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			rig2d.AddForce (swingForce * Vector2.left);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			rig2d.AddForce (swingForce * Vector2.right);
		}
	}
}
