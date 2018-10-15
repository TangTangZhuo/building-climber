using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
	Rigidbody2D hookRig2D;
	// Use this for initialization
	void Start () {
		hookRig2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (transform.parent == null) {
			Transform obj = coll.transform.parent;
			if (obj.name == "RocketCollider") {
				transform.SetParent (obj.parent);
				hookRig2D.bodyType = RigidbodyType2D.Kinematic;
			}
		}
	}
}
