using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			animator.SetBool ("goBuilding", true);

		}
		if (Input.GetKeyDown (KeyCode.R)) {
			animator.SetBool ("goBuilding", false);
		}
	}
}
