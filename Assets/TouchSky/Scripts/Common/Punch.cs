using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Punch : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		StartCoroutine (DoPunch (1));
	}
	
	IEnumerator DoPunch(float time){
		while (true) {
			transform.DOPunchRotation(new Vector3(0,0,5),time,5,1);
			yield return  new WaitForSeconds (time+0.3f);
		}
	}
}
