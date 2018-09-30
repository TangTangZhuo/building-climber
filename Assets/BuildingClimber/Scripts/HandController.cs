using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandController : MonoBehaviour {

	public Transform RHand2;
	public Transform RHand1;

	public float maxRotation = 80;

	Vector3 currentRotation2;
	Vector3 currentRotation1;
	bool isDoRotate = false;

	void Start () {
		currentRotation1 = RHand1.rotation.eulerAngles;
		currentRotation2 = RHand2.rotation.eulerAngles;
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			if (!isDoRotate) {
				isDoRotate = true;
				RHand2.DORotate (new Vector3 (0, 0, currentRotation2.z + maxRotation), 0.5f, 0).OnComplete (() => {
					maxRotation = -maxRotation;
					isDoRotate = false;
				});
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			RHand2.DOKill ();
			isDoRotate = false;
			RHand2.DORotate (new Vector3 (0, 0, currentRotation2.z + Mathf.Abs(maxRotation)), 0.2f, 0).OnComplete (() => {				
				RHand1.DORotate(new Vector3(0,0,currentRotation1.z-170),0.3f,0).OnComplete(()=>{					
					RHand1.DORotate(new Vector3(0,0,currentRotation1.z),1f,0).SetDelay(1);				
				});

			});

		}
	}
}
