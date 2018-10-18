using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ForwardTest : MonoBehaviour {
	public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {
			//transform.LookAt (target.transform);
			transform.rotation = Quaternion.LookRotation(Vector3.forward,target.transform.position-transform.position);
			//transform.Rotate(new Vector3(90,90,0)); 
		}
			
	}
}
