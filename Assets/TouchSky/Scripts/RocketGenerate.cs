using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
		GenerateRocket (8);

	}
		

	void GenerateRocket(int number){
		rocketCount = rocketParent.transform.childCount;
		if (rocketCount < number) {
			Vector3 rangeV3 = new Vector3 (Random.Range (-8f, 8f), Random.Range (-10f, 24f), 0);
			generatePos = new Vector3 (transform.position.x, player.transform.position.y, transform.position.z) + rangeV3;
			GameObject go = GameObject.Instantiate (rocket, generatePos, rocket.transform.rotation, rocketParent.transform);
			go.GetComponent<FlyController> ().speed = Random.Range (1f, 6f);
			go.GetComponent<SpriteRenderer> ().DOFade (1, 0.3f).OnComplete(()=>{
				go.transform.Find("RocketCollider").gameObject.SetActive(true);
			});
		}

	}
}
