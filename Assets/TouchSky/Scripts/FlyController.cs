using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour {
	public float speed = 10;
	Transform player;
	RocketGenerate rocketGenerate;
	Transform otherRocket;

	AI_ThrowHook ai_throwHook;
	ThrowHook throwHook;

	AI_ThrowHook[] ai_throwHooks;

	void Start () {
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("AI");
		ai_throwHooks = new AI_ThrowHook[objs.Length];
		for (int i = 0; i < objs.Length; i++) {
			ai_throwHooks[i] = objs [i].GetComponent<AI_ThrowHook> ();
		}

		rocketGenerate = FindObjectOfType (typeof(RocketGenerate))as RocketGenerate;

		player = GameObject.FindGameObjectWithTag ("Player").transform;
		otherRocket = GameObject.FindGameObjectWithTag ("OtherRocket").transform;
		if (transform.parent) {
			if (transform.parent.name.StartsWith ("AI")) {
				player = GameObject.Find (transform.parent.name.Split ('_') [0]).transform;
				ai_throwHook = player.GetComponent<AI_ThrowHook> ();
			} else {
				player = GameObject.FindGameObjectWithTag ("Player").transform;
				throwHook = player.GetComponent<ThrowHook> ();
			}
		}
		StartCoroutine (CheckDistance ());
	}
	
	void Update () {
		if (transform.position.x < -10.5f) {
			transform.position = new Vector3 (-10.5f, transform.position.y, transform.position.z);
		}
		if (transform.position.x > 7.5f) {
			transform.position = new Vector3 (7.5f, transform.position.y, transform.position.z);
		}
		//transform.position += Vector3.up * speed * Time.deltaTime;

		transform.position = Vector3.Lerp (transform.position, transform.position + Vector3.up, speed * Time.deltaTime);

//			if (ai_throwHook && ai_throwHook.hookTarget == transform) {				
//			}else if(throwHook && throwHook.hookTarget == transform){				
//			}else {
//				Destroy (gameObject);
//			}

//			for (int i = 0; i < ai_throwHooks.Length; i++) {
//
//				if (ai_throwHooks [i] && ai_throwHooks [i].hookTarget == transform) {
//				} else {
//					Destroy (gameObject);
//				}
//
//			}
//
//			if (throwHook && throwHook.hookTarget == transform) {
//				
//			} else {
//				Destroy (gameObject);
//			}
//		}
	}

	IEnumerator CheckDistance(){
		while (true) {			
			if (transform.position.y - rocketGenerate.maxTrans.position.y > 24 || rocketGenerate.minTrans.position.y - transform.position.y > 14) {			
				Destroy (gameObject);
			}
			if (transform.position.y - player.position.y > 24 || player.position.y - transform.position.y > 14) {
				transform.SetParent (otherRocket);						
			}
			yield return new WaitForSeconds (0.2f);
		}
	}

}
