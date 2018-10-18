using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour {
	public float speed = 10;
	Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position += Vector3.up * speed * Time.deltaTime;
		transform.position = Vector3.Lerp (transform.position, transform.position + Vector3.up, speed * Time.deltaTime);
		if (Mathf.Abs (player.position.y - transform.position.y) > 25) {
			
			Destroy (gameObject);
		}
	}


}
