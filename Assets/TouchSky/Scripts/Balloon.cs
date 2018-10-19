using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Balloon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (BallSimulate ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator BallSimulate(){
		while (this) {
			transform.DOPunchPosition (Vector3.up / 2, 5f, 0, 0, false);
			yield return new WaitForSeconds (5);
		}
	}
}
