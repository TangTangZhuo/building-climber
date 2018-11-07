using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketCollider : MonoBehaviour {
	ThrowHook throwHook;
	AI_ThrowHook[] ai_throwHooks;

	//public string aiName = "";

	void OnEnable(){
//		print (aiName);
//		if (aiName.StartsWith ("AI")) {
//			ai_throwHook = GameObject.Find (aiName).GetComponent<AI_ThrowHook> ();
//		}
	}

	// Use this for initialization
	void Start () {
		throwHook = GameObject.FindGameObjectWithTag ("Player").GetComponent<ThrowHook>();
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("AI");
		ai_throwHooks = new AI_ThrowHook[objs.Length];
		for (int i = 0; i < objs.Length; i++) {
			ai_throwHooks[i] = objs [i].GetComponent<AI_ThrowHook> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
	//	print (ai_throwHook);
		//if (throwHook.hookTarget == transform.parent.parent) {
			if (transform.tag == "curRocket") {
				if (throwHook.gameState == GameState.isHooking) {
				if (coll.tag == "deadline" || coll.tag == "deadcloud" || coll.tag == "rocket") {
					Instantiate (ParticleManager.Instance.particle_rocketDead, transform.position, transform.rotation);
					transform.parent.parent.GetComponent<SpriteRenderer> ().enabled = false;
					if (throwHook.hookTarget == transform.parent.parent) {
						Camera.main.transform.DOShakePosition (0.4f, 1, 10, 90, false, true);
						//Invoke ("GameOverPre", 0.4f);
						throwHook.GetComponent<PlayerCollider>().GameOverPre();
						Destroy (transform.parent.parent.gameObject);
					}

				}
				if (coll.tag == "balloon") {
					if (throwHook.hookTarget == transform.parent.parent) {
						Instantiate (ParticleManager.Instance.particle_Gift, coll.transform.position, coll.transform.rotation);
						Destroy (coll.gameObject);
						GetReward ();
						GameObject.Find ("FlyTreasureRun").GetComponent<FlyGold> ().FlyGoldGenerate (1);
					}
				}
				
				
				}
			}

		//AI碰撞
		if (transform.tag == "curRocket") {
			if (coll.tag == "deadcloud" || coll.tag == "rocket") {									
				for (int i = 0; i < ai_throwHooks.Length; i++) {
					if (ai_throwHooks [i].hookTarget == transform.parent.parent) {
						
						Instantiate (ParticleManager.Instance.particle_rocketDead, transform.position, transform.rotation);
						transform.parent.parent.GetComponent<SpriteRenderer> ().enabled = false;								

						Destroy (ai_throwHooks [i].curHook);
						ai_throwHooks [i].gameState = AI_ThrowHook.AIState.isInSky;

						Destroy (this.transform.parent.parent.gameObject);
						ai_throwHooks [i].GenerateCircle ();
											
					}
				}
					
			}
				
		}
		//}
	}

	void GetReward(){
		ProgressSlider.Instance.AddTreasure (1);
	}

//	void GameOverPre(){
//		Time.timeScale = 0.2f;
//		Time.fixedDeltaTime = 0.02f * Time.timeScale;
//		Invoke ("GameOver", 0.2f);
//	}
//
//	void GameOver(){
//		PlayerControllerSky.GameEnd ();
//	}
}
