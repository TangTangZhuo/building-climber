using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGenerate : MonoBehaviour {

	public GameObject rocket;
	public GameObject player;
	public GameObject rocketParent;
	public int rocketCount = 0;
	Vector3 generatePos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GenerateRocket (10);
	}
		

	void GenerateRocket(int number){
		rocketCount = rocketParent.transform.childCount;
		if (rocketCount < number) {
			Vector3 rangeV3 = new Vector3 (Random.Range (-6f, 6f), Random.Range (-24f, 24f), 0);
			generatePos = new Vector3 (transform.position.x, player.transform.position.y, transform.position.z) + rangeV3;
			GameObject go = GameObject.Instantiate (rocket, generatePos, rocket.transform.rotation, rocketParent.transform);
			go.GetComponent<FlyController> ().speed = Random.Range (1f, 3f);
		}

	}
}
