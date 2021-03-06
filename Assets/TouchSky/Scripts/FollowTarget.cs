﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class FollowTarget : MonoBehaviour {
	public Transform target;
	public Vector3 offset = Vector3.zero;
	ThrowHook throwHook;
	// Use this for initialization
	void Start () {
		throwHook = GameObject.FindGameObjectWithTag ("Player").GetComponent<ThrowHook>();

	}
	
	// Update is called once per frame
	void Update () {
		if (throwHook.gameState == GameState.isHooking || throwHook.gameState == GameState.isTakeBacking) {
			if (throwHook.ropeSriptes) {
				target = throwHook.ropeSriptes.transform;
			}
		} else if (throwHook.gameState == GameState.isShooting || throwHook.gameState == GameState.isInSky) {
			target = throwHook.transform;
		}
		if (target != null) {
			Vector3 targetV3 = new Vector3 (target.position.x+offset.x, target.position.y + offset.y, transform.position.z);
			transform.position = Vector3.Lerp (transform.position, targetV3, Time.deltaTime*2);
		}
	}
}
