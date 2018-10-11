using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour {

	public Transform followTarget;
//	public bool isThrow = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (followTarget != null) {
			transform.position = followTarget.position;
//			if (isThrow)
			transform.rotation = followTarget.rotation;
		}
	}
}
