using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketGenerate : MonoBehaviour {

	public GameObject rocket;
	public GameObject player;
	public GameObject[] AIs; 
	public GameObject rocketParent;
	[HideInInspector]
	public Transform maxTrans;
	[HideInInspector]
	public Transform minTrans;
	GameObject[] parents;
	public int rocketCount = 0;
	Vector3 generatePos;
	// Use this for initialization
	void Awake () {
		parents = new GameObject[AIs.Length];
		AIs = GameObject.FindGameObjectsWithTag ("AI");
		for (int i = 0; i < AIs.Length; i++) {
			parents[i] = new GameObject (AIs [i].name+"_parent");
		}
		maxTrans = MaxPlayer ();
		minTrans = MinPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		FinalGenetateRocket ();
		maxTrans = MaxPlayer ();
		minTrans = MinPlayer ();

	}
		
	Transform MaxPlayer(){
		Transform temp = AIs[0].transform;
		for (int i = 0; i < AIs.Length-1; i++) {			
			if (temp.position.y <= AIs [i + 1].transform.position.y) {
				temp = AIs [i + 1].transform;
			}
		}
		if (temp.transform.position.y <= player.transform.position.y) {
			temp = player.transform;
		}
		return temp;
	}

	Transform MinPlayer(){
		Transform temp = AIs[0].transform;
		for (int i = 0; i < AIs.Length-1; i++) {			
			if (temp.position.y >= AIs [i + 1].transform.position.y) {
				temp = AIs [i + 1].transform;
			}
		}
		if (temp.transform.position.y >= player.transform.position.y) {
			temp = player.transform;
		}
		return temp;
	}

	bool NoOthers(Transform trans){
		for (int i = 0; i < AIs.Length-1; i++) {		
			if (trans != AIs [i]) {	
				if (Mathf.Abs (trans.position.y - AIs [i].transform.position.y) > 15)
					return true;
			}
		}
		if (Mathf.Abs (trans.position.y - player.transform.position.y) > 15)
			return true;
		else
			return false;
	}

	void FinalGenetateRocket(){
		
		GenerateRocket (8, player.transform, rocketParent.transform);


		for (int i = 0; i < AIs.Length; i++) {
			if (NoOthers (AIs [i].transform)) {
				GenerateRocket (8, AIs [i].transform, parents [i].transform);
			}
		}
	}

	void GenerateRocket(int number,Transform pos,Transform parent){
		rocketCount = parent.childCount;
		if (rocketCount < number) {
			Vector3 rangeV3 = new Vector3 (Random.Range (-9f, 9f), Random.Range (-10f, 24f), 0);
			generatePos = new Vector3 (transform.position.x, pos.position.y, transform.position.z) + rangeV3;
			GameObject go = GameObject.Instantiate (rocket, generatePos, rocket.transform.rotation, parent);
			go.GetComponent<FlyController> ().speed = Random.Range (1f, 6f);
			go.GetComponent<SpriteRenderer> ().DOFade (1, 0.3f).OnComplete(()=>{
				//go.transform.Find("RocketCollider").GetComponentInChildren<RocketCollider>().aiName = pos.name;
				go.transform.Find("RocketCollider").gameObject.SetActive(true);
			});
		}

	}
}
