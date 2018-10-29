//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;
//
//public class AI_RocketCollider : MonoBehaviour {
//	AI_ThrowHook ai_throwHook;
//	public string aiName = "";
//
//	void OnEnable(){
//				print (aiName);
//		ai_throwHook = GameObject.Find (aiName).GetComponent<AI_ThrowHook> ();
//	}
//
//	// Update is called once per frame
//	void Update () {
//
//	}
//
//	void OnTriggerEnter2D(Collider2D coll){
//		//if (throwHook.hookTarget == transform.parent.parent) {
//		if (transform.tag == "curRocket") {
//			if (throwHook.gameState == GameState.isHooking) {
//				if (coll.tag == "deadline" || coll.tag == "deadcloud" || coll.tag == "rocket") {
//					Instantiate (ParticleManager.Instance.particle_rocketDead, transform.position, transform.rotation);
//					transform.parent.parent.GetComponent<SpriteRenderer> ().enabled = false;
//					if (throwHook.hookTarget == transform.parent.parent) {
//						Camera.main.transform.DOShakePosition (0.4f, 1, 10, 90, false, true);
//						Invoke ("GameOverPre", 0.4f);
//					} else {						
//						Destroy (this.transform.parent.parent.gameObject);
//					}
//				}
//				if (coll.tag == "balloon") {
//					if (throwHook.hookTarget == transform.parent.parent) {
//						Instantiate (ParticleManager.Instance.particle_Gift, coll.transform.position, coll.transform.rotation);
//						Destroy (coll.gameObject);
//						GetReward ();
//					}
//				}
//
//
//			}
//		}
//		//}
//	}
//
//	void GetReward(){
//
//	}
//
//	void GameOverPre(){
//		Time.timeScale = 0.2f;
//		Time.fixedDeltaTime = 0.02f * Time.timeScale;
//		Invoke ("GameOver", 0.2f);
//	}
//
//	void GameOver(){
//		PlayerControllerSky.GameOver ();
//	}
//}
