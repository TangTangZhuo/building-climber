using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWall : MonoBehaviour {
	public Transform target;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
		if (target != null) {
			Vector3 targetV3 = new Vector3 (transform.position.x, target.position.y, transform.position.z);
			transform.position = targetV3;
		}
	}
}
