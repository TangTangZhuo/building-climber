using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HookManager : MonoBehaviour {
	LineRenderer lineRenderer;
	Transform hook;
	Transform rightHand;

	Vector3 startPos;
	Vector3 endPos;

	bool isDoRotate = false;
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		hook = GameObject.FindGameObjectWithTag ("hook").transform;
		rightHand = GameObject.FindGameObjectWithTag ("RightHand").transform;
	}
	
	// Update is called once per frame
	void Update () {
		NormalUpdate ();
	}

	void ThrowUpdate(){
		
	}

	void NormalUpdate(){
		hook.RotateAround (rightHand.position, Vector3.left, 10);
		//hook.position = new Vector3(rightHand.position.x,hook.position.y,hook.position.z);
		//hook.parent.rotation = rightHand.rotation;

		startPos = rightHand.position;
		endPos = hook.position;
		lineRenderer.positionCount = 2;
		lineRenderer.SetPosition (0, startPos);
		lineRenderer.SetPosition (1, endPos);
	}
}
