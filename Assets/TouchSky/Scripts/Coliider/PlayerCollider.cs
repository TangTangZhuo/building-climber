using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCollider : MonoBehaviour {
	ThrowHook throwHook;
	Rigidbody2D rig2D;
	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		throwHook = GetComponent<ThrowHook> ();
		rig2D = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (throwHook.gameState == GameState.isHooking) {
			if (coll.tag == "deadline" || coll.tag == "rocket") {
				rig2D.AddForce ((Vector2)(transform.position - coll.transform.position).normalized * 1000);
				Instantiate (ParticleManager.Instance.particle_playerHit, transform.position, transform.rotation);
			}

		}
		if (throwHook.gameState != GameState.isHooking) {
			if (coll.tag == "deadline") {
				Instantiate (ParticleManager.Instance.particle_playerDead, transform.position, transform.rotation);
				Camera.main.transform.DOShakePosition (0.4f, 1, 10, 90, false, true);
				GameOverPre ();
			}
		}	

		if (coll.tag == "deadcloud") {
			Instantiate (ParticleManager.Instance.particle_playerEle, transform.position, transform.rotation).transform.parent = transform;
			sr.color = Color.black;
			sr.DOColor (Color.red, 2f);

		}

		if (coll.tag == "balloon") {
			Instantiate (ParticleManager.Instance.particle_Gift, coll.transform.position, coll.transform.rotation);
			Destroy (coll.gameObject);
			GetReward ();
		}

	}

	void GetReward(){
		
	}

	void GameOverPre(){
		Time.timeScale = 0.2f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
		Invoke ("GameOver", 0.4f);
	}

	void GameOver(){
		PlayerControllerSky.GameOver ();
	}
}
