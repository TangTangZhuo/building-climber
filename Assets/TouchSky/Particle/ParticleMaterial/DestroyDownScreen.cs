using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDownScreen : MonoBehaviour {
	Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {		
		if (player.position.y - transform.position.y > 8) {
			Destroy (gameObject);
		}
	}
}
