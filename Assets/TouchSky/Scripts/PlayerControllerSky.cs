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
		
	}

	void OnTriggerEnter2D(Collider2D coll){
//		if (coll.tag == "deadline") {
//			Time.timeScale = 0.2f;
//			Time.fixedDeltaTime = 0.02f * Time.timeScale;
//			Invoke ("GameOver", 0.2f);
//		}
	}

	public static void GameOver(){
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f * 0.35f;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
