using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerSky : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -10.5f) {
			transform.position = new Vector3 (-10.5f, transform.position.y, transform.position.z);
		}
		if (transform.position.x > 7.5f) {
			transform.position = new Vector3 (7.5f, transform.position.y, transform.position.z);
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
//		if (coll.tag == "deadline") {
//			Time.timeScale = 0.2f;
//			Time.fixedDeltaTime = 0.02f * Time.timeScale;
//			Invoke ("GameOver", 0.2f);
//		}
	}

	public static void GameEnd(){
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f * 0.35f;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
