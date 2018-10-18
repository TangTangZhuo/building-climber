using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCircle : MonoBehaviour {
	
	ThrowHook throwHook;

	// Use this for initialization
	void Start () {
		throwHook = GameObject.FindGameObjectWithTag ("Player").GetComponent<ThrowHook> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "rocket") {
			if (throwHook.curRocket) {
				throwHook.curRocket.GetComponent<SpriteRenderer> ().color = Color.white;
			}
			throwHook.curRocket = coll.transform.parent.parent.gameObject;
			throwHook.curRocket.GetComponent<SpriteRenderer> ().color = Color.black;
			Time.timeScale = 0.2f;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "rocket") {
			if (throwHook.curRocket && throwHook.curRocket == coll.transform.parent.parent.gameObject) {
				throwHook.curRocket.GetComponent<SpriteRenderer> ().color = Color.white;
				throwHook.curRocket = null;
				Time.timeScale = 1f;
			}
		}
	}


}
