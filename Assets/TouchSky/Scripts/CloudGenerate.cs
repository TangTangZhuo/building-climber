using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerate : MonoBehaviour {
	public GameObject cloud_small;
	public GameObject cloud_big;

	public GameObject area2_small;
	public GameObject area2_mid;
	public GameObject area2_big;

	public GameObject area3_small;
	public GameObject area3_mid;
	public GameObject area3_big;

	public GameObject balloon;
	public GameObject player;

	public GameObject parent;
	GameObject smallParent;
	GameObject bigParent;
	GameObject midParent;
	GameObject balloonParent;

	[HideInInspector]
	public int smallCount = 0;
	[HideInInspector]
	public int bigCount = 0;
	[HideInInspector]
	public int midCount = 0;
	[HideInInspector]
	public int balloonCount = 0;
	[HideInInspector]
	public int areaIndex = 1;

	static CloudGenerate instance;
	public static CloudGenerate Instance{
		get{return instance;}
	}
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		smallParent = parent.transform.Find ("smallParent").gameObject;
		bigParent = parent.transform.Find ("bigParent").gameObject;
		midParent = parent.transform.Find ("midParent").gameObject;

		balloonParent = parent.transform.Find ("balloonParent").gameObject;

		ViewLB = Camera.main.ViewportToWorldPoint (new Vector3(0,0,0));
		ViewRU =  Camera.main.ViewportToWorldPoint (new Vector3(1,1,1));
	}
	
	// Update is called once per frame
	void Update () {
		if (areaIndex == 1) {
			Generate (4, smallCount, smallParent, cloud_small);
			Generate (1, bigCount, bigParent, cloud_big);
		}

		if (areaIndex == 2) {
			Generate (3, smallCount, smallParent, area2_small);
			Generate (1, bigCount, bigParent, area2_big);
			Generate (2, midCount, midParent, area2_mid);
		}

		if (areaIndex == 3) {
			Generate (3, smallCount, smallParent, area3_small);
			Generate (1, bigCount, bigParent, area3_big);
			Generate (2, midCount, midParent, area3_mid);
		}

		Generate (1, balloonCount, balloonParent, balloon);
	}
		
	Vector3 ViewLB = Vector3.zero;
	Vector3 ViewRU = Vector3.zero;

	void Generate(int number,int count,GameObject parent,GameObject child){
		count = parent.transform.childCount;
		if (count < number) {
			Vector3 rangeV3 = new Vector3 (Random.Range ((ViewLB.x-ViewRU.x)/1.5f,(ViewRU.x-ViewLB.x)/1.5f), 
				Random.Range (ViewRU.y, ViewRU.y*8), 0);
			Vector3 generatePos = player.transform.position + rangeV3;
			//Vector3 rangeV3 = new Vector3 (Random.Range (-8f, 8f), Random.Range (10f, 150f), 0);

			//Vector3 generatePos = new Vector3 (transform.position.x, player.transform.position.y, transform.position.z) + rangeV3;
			GameObject go = GameObject.Instantiate (child, generatePos, child.transform.rotation, parent.transform);

		}
	}

	public void DestroyObstacle(){
		for(int i=0;i<midParent.transform.childCount;i++){
			Destroy (midParent.transform.GetChild(i).gameObject);
		}
		for(int i=0;i<smallParent.transform.childCount;i++){
			Destroy (smallParent.transform.GetChild(i).gameObject);
		}
		for(int i=0;i<bigParent.transform.childCount;i++){
			Destroy (bigParent.transform.GetChild(i).gameObject);
		}
	}
}
