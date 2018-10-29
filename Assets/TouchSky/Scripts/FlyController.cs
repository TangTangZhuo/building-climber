﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour {
	public float speed = 10;
	Transform player;
	RocketGenerate rocketGenerate;
	Transform otherRocket;

	AI_ThrowHook ai_throwHook;
	ThrowHook throwHook;

	void Start () {
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

	}
	
	void Update () {
		//transform.position += Vector3.up * speed * Time.deltaTime;
		transform.position = Vector3.Lerp (transform.position, transform.position + Vector3.up, speed * Time.deltaTime);
		if (transform.position.y - rocketGenerate.maxTrans.position.y > 24||rocketGenerate.minTrans.position.y - transform.position.y > 14) {			
			Destroy (gameObject);
		}
		if (transform.position.y - player.position.y > 24 || player.position.y - transform.position.y > 14) {
			//transform.SetParent (otherRocket);
			if (ai_throwHook && ai_throwHook.hookTarget == transform) {				
			}else if(throwHook && throwHook.hookTarget == transform){				
			}else {
				Destroy (gameObject);
			}
		}
	}



}
