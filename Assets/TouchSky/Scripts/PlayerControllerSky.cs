using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerSky : MonoBehaviour {

	float screenLeft = 0;
	float screenRight = 0;

	// Use this for initialization
	void Start () {
		screenLeft = RocketColorManager.Instance.screenLeft.position.x;
		screenRight = RocketColorManager.Instance.screenRight.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < screenLeft) {
			transform.position = new Vector3 (screenLeft, transform.position.y, transform.position.z);
		}
		if (transform.position.x > screenRight) {
			transform.position = new Vector3 (screenRight, transform.position.y, transform.position.z);
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
