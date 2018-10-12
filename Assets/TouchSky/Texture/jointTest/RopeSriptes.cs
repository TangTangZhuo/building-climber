using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSriptes : MonoBehaviour {
	public Vector2 destiny;
	public float speed = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards (transform.position, destiny, speed);
	}
}
