using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerate : MonoBehaviour {
	public GameObject cloud_small;
	public GameObject cloud_big;
	public GameObject balloon;
	public GameObject player;

	public GameObject parent;
	GameObject smallParent;
	GameObject bigParent;
	GameObject balloonParent;

	[HideInInspector]
	public int smallCount = 0;
	[HideInInspector]
	public int bigCount = 0;
	[HideInInspector]
	public int balloonCount = 0;

	// Use this for initialization
	void Start () {
		smallParent = parent.transform.Find ("smallParent").gameObject;
		bigParent = parent.transform.Find ("bigParent").gameObject;
		balloonParent = parent.transform.Find ("balloonParent").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Generate (5, smallCount, smallParent, cloud_small);
		Generate (2, bigCount, bigParent, cloud_big);
		Generate (1, balloonCount, balloonParent, balloon);
	}
		

	void Generate(int number,int count,GameObject parent,GameObject child){
		count = parent.transform.childCount;
		if (count < number) {
			Vector3 rangeV3 = new Vector3 (Random.Range (-8f, 8f), Random.Range (10f, 150f), 0);
			Vector3 generatePos = new Vector3 (transform.position.x, player.transform.position.y, transform.position.z) + rangeV3;
			GameObject go = GameObject.Instantiate (child, generatePos, child.transform.rotation, parent.transform);

		}
	}
}
