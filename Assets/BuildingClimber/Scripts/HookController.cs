﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour {
	Animator animator;
	PlayerController player;
	ChangeRopeLength rope;

	bool jumpfinish = true;

	// Use this for initialization
	void Start () {
		player = PlayerController.Instance;
		rope = ChangeRopeLength.Instance;
		animator = player.animator;
		player.jumpFinish += () => {
			rope.SetStiffness (1);
			jumpfinish = true;
		};

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		
		if (jumpfinish) {
			jumpfinish = false;
			if (col.tag == "normalWall" || col.tag == "RogueWall" || col.name == "thorns") {
				
				animator.SetTrigger ("TriggerWall");
				rope.SetStiffness (0);

				player.PlayerJump (5, transform.position.y - player.transform.position.y);
			}				
		}
		//if (jumpfinish) {

//			if (col.tag == "windowWall") {
//			//	if (!animator.GetCurrentAnimatorStateInfo (1).IsName ("Jump")) {
//					animator.SetTrigger ("TriggerWindowOrEagle");
//					rope.resetRope = true;
//			//	}
//			}
		//}
	}
		
}
