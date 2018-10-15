using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : MonoBehaviour {
	public GameObject hook;

	GameObject curHook;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector2 destiny = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float direction = VectorAngle((Vector2)transform.position,destiny);
			curHook = (GameObject)Instantiate (hook, transform.position, Quaternion.Euler(new Vector3(0,0,direction)));
			curHook.GetComponent<RopeSriptes> ().destiny = destiny;
		}
	}

	float VectorAngle(Vector2 from, Vector2 to)
	{
		float angle;		        
		Vector3 cross = Vector3.Cross (from, to);
		angle = Vector2.Angle (from, to)+180;
		return cross.z > 0 ? angle : -angle;
	}


}
