using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCircle : MonoBehaviour {
	
	ThrowHook throwHook;

	public Material black;
	public Material write;

	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
		
		throwHook = GameObject.FindGameObjectWithTag ("Player").GetComponent<ThrowHook> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "rocket") {
			if (throwHook.curRocket!=null) {
				//throwHook.curRocket.GetComponent<SpriteRenderer> ().color = Color.white;
				//throwHook.curRocket.GetComponent<SpriteRenderer> ().material = black;
				Transform down = throwHook.curRocket.transform.Find ("rocket3D").Find ("Down");
				Transform up = throwHook.curRocket.transform.Find ("rocket3D").Find ("Up");
				down.GetComponent<MeshRenderer>().material = black;
				up.GetComponent<MeshRenderer>().material = black;
				
			}
			throwHook.curRocket = coll.transform.parent.parent.gameObject;
			//throwHook.curRocket.GetComponent<SpriteRenderer> ().color = Color.black;
			throwHook.curRocket.GetComponent<SpriteRenderer> ().material = write;

			Transform down1 = throwHook.curRocket.transform.Find ("rocket3D").Find ("Down");
			Transform up1 = throwHook.curRocket.transform.Find ("rocket3D").Find ("Up");
			down1.GetComponent<MeshRenderer>().material = write;
			up1.GetComponent<MeshRenderer>().material = write;

			Time.timeScale = 0.2f;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "rocket") {
			if (throwHook.curRocket && throwHook.curRocket == coll.transform.parent.parent.gameObject) {
				//throwHook.curRocket.GetComponent<SpriteRenderer> ().color = Color.white;
				//throwHook.curRocket.GetComponent<SpriteRenderer> ().material = black;

				Transform down = throwHook.curRocket.transform.Find ("rocket3D").Find ("Down");
				Transform up = throwHook.curRocket.transform.Find ("rocket3D").Find ("Up");
				down.GetComponent<MeshRenderer>().material = black;
				up.GetComponent<MeshRenderer>().material = black;

				throwHook.curRocket = null;
				Time.timeScale = 1f;
				Time.fixedDeltaTime = 0.02f * 0.35f;
			}
		}
	}


}
