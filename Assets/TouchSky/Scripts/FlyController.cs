using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour {
	public float speed = 10;
	Transform player;
	RocketGenerate rocketGenerate;

	void Start () {
		rocketGenerate = FindObjectOfType (typeof(RocketGenerate))as RocketGenerate;

		player = GameObject.FindGameObjectWithTag ("Player").transform;
		if (transform.parent) {
			if (transform.parent.name.StartsWith ("AI")) {
				player = GameObject.Find (transform.parent.name.Split ('_') [0]).transform;
			} else {
				player = GameObject.FindGameObjectWithTag ("Player").transform;
			}
		}
	}
	
	void Update () {
		//transform.position += Vector3.up * speed * Time.deltaTime;
		transform.position = Vector3.Lerp (transform.position, transform.position + Vector3.up, speed * Time.deltaTime);
		if (transform.position.y - rocketGenerate.maxTrans.position.y > 24||rocketGenerate.minTrans.position.y - transform.position.y > 14) {			
			Destroy (gameObject);
		}
	}


}
