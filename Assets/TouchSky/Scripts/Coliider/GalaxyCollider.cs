using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GalaxyCollider : MonoBehaviour {
	ThrowHook throwHook;

	// Use this for initialization
	void Start () {
		throwHook = GameObject.FindGameObjectWithTag ("Player").GetComponent<ThrowHook>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "Player" || (coll.tag == "curRocket"&&throwHook.hookTarget == coll.transform.parent.parent)) {
			//transform.parent.GetComponent<ParticleSystem>().main.startSpeedMultiplier = Mathf.Lerp()
			ParticleSystem ps = transform.parent.GetComponent<ParticleSystem> ();
			var noise = ps.noise;
			if (noise.enabled == false) {
				noise.enabled = true;
				Time.timeScale = 0.5f;
			}
				
			if(transform.name == "1"){
				CloudGenerate.Instance.areaIndex = 2;
			}
			if(transform.name == "2"){
				CloudGenerate.Instance.areaIndex = 3;
			}
			CloudGenerate.Instance.DestroyObstacle ();
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "Player"|| (coll.tag == "curRocket"&&throwHook.hookTarget == coll.transform.parent.parent)) {
			if (Time.timeScale == 0.5f)
				Time.timeScale = 1f;
			Destroy (transform.parent.gameObject,5);
		}
	}

	IEnumerator GalaxyColl(float time){
		float timer = 0;
		ParticleSystem ps = transform.parent.GetComponent<ParticleSystem> ();
		var main = ps.main;
		while (true) {			
			main.startSpeed = Mathf.Lerp (0, 2, timer += Time.deltaTime/time);
			if (timer > 1) {
				yield break;
			}
			yield return null;
		}
	}
}
