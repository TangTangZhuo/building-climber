using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagAddForce : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().AddForce (Vector3.left*100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
