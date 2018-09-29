using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRope : MonoBehaviour {

	public Transform[] bones;
	public float speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Q)) {
			ChangeLength (false);
		}
		if (Input.GetKey (KeyCode.W)) {
			ChangeLength (true);
		}
	}

	void CutLength(){
		for (int i = bones.Length - 1; i >= 1; i--) {
			
				//bones [i].position = Vector3.Lerp (bones [i].position, bones [i - 1].position, speed * Time.deltaTime);
			Vector3 dir = bones[i-1].position-bones[i].position;
			bones [i].position = Vector3.Lerp (bones [i].position, bones [i].position + dir, speed * Time.deltaTime);
		}
	}

	void AddLength(){
		for (int i = bones.Length-1; i >= 1; i--) {			
			//Vector3 dir = bones[i+1].position-bones[i].position;
			//bones [i].position = Vector3.Lerp (bones [i].position, bones [i + 1].position + dir, speed * Time.deltaTime);
			Vector3 dir = bones[i].position-bones[i-1].position;
			bones [i].position = Vector3.Lerp (bones [i].position, bones [i].position + dir, speed * Time.deltaTime);
		}
	}

	void ChangeLength(bool state){
		for (int i = bones.Length - 1; i >= 1; i--) {
			Vector3 dir = Vector3.one;
			if (state == true) {
				dir = bones [i - 1].position - bones [i].position;
			} else {
				dir = bones [i].position - bones [i - 1].position;
			}
			bones [i].position = Vector3.Lerp (bones [i].position, bones [i].position + dir, speed * Time.deltaTime);
		}


	}
}
