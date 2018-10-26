using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_InCircle : MonoBehaviour {

	AI_ThrowHook ai_throwHook;
	public string aiName = "";

	void OnEnable(){
//		print (aiName);
		ai_throwHook = GameObject.Find (aiName).GetComponent<AI_ThrowHook> ();
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "rocket") {			
			ai_throwHook.curRocket = coll.transform.parent.parent.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "rocket") {
			if (ai_throwHook.curRocket && ai_throwHook.curRocket == coll.transform.parent.parent.gameObject) {
				ai_throwHook.curRocket = null;
			}
		}
	}


}
