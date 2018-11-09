using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCircle : MonoBehaviour {
	
	ThrowHook throwHook;

	public Material black;
	public Material write;


	void Awake(){
		
		throwHook = GameObject.FindGameObjectWithTag ("Player").GetComponent<ThrowHook> ();
	}


	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "rocket") {
			if (throwHook.curRocket!=null) {
				ChangeRocketColor(black);
				
			}
			throwHook.curRocket = coll.transform.parent.parent.gameObject;
//			//throwHook.curRocket.GetComponent<SpriteRenderer> ().color = Color.black;
			throwHook.curRocket.GetComponent<SpriteRenderer> ().material = write;

			ChangeRocketColor(write);

			Time.timeScale = 0.2f;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "rocket") {
			if (throwHook.curRocket && throwHook.curRocket == coll.transform.parent.parent.gameObject) {

				ChangeRocketColor (black);

				throwHook.curRocket = null;
				Time.timeScale = 1f;
				Time.fixedDeltaTime = 0.02f * 0.35f;
			}
		}
	}


	void ChangeRocketColor(Material color){

		Transform C_up1 = throwHook.curRocket.transform.Find ("RocketNew").Find ("C_up1");
		Transform C_down2 = throwHook.curRocket.transform.Find ("RocketNew").Find ("C_down2");
		Transform C_left = throwHook.curRocket.transform.Find ("RocketNew").Find ("C_left");
		Transform C_right = throwHook.curRocket.transform.Find ("RocketNew").Find ("C_right");

		C_up1.GetComponent<MeshRenderer>().material = color;
		C_down2.GetComponent<MeshRenderer>().material = color;
		C_left.GetComponent<MeshRenderer>().material = color;
		C_right.GetComponent<MeshRenderer>().material = color;
	}

}
