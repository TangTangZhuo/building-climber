using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowTarget : MonoBehaviour {
	public Transform target;
	public Vector3 offset = Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector3 targetV3 = new Vector3 (transform.position.x, target.position.y + offset.y, transform.position.z);
			transform.position = Vector3.Lerp (transform.position, targetV3, Time.deltaTime);
		}
	}
}
