using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCollider : MonoBehaviour {
	ThrowHook throwHook;
	Rigidbody2D rig2D;
	SpriteRenderer sr;

	public GameObject progress;
	public GameObject rankPop;
	public Transform BG;

	// Use this for initialization
	void Start () {
		throwHook = GetComponent<ThrowHook> ();
		rig2D = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		StartCoroutine (UpdateDrop ());
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

		if (coll.tag == "Terminal") {
			GameWin ();
		}

		if (coll.tag == "BGTrigger") {
			Destroy (coll.gameObject);
			StartCoroutine (MoveBG());
		}
	}

	IEnumerator MoveBG(){
		Vector3 target = BG.localPosition + Vector3.down * 15;
		while (true) {
			BG.localPosition = Vector3.Lerp (BG.localPosition, target, Time.deltaTime);
			print (BG.localPosition.y);
			print (target.y);
			if (BG.localPosition.y<target.y) {
				
				yield break;
			}
			yield return null;
		}
	}

	void GetReward(){
		
	}

	public void GameOverPre(){
		Time.timeScale = 0.2f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
		Invoke ("GameEnd", 0.4f);
	}

	void GameEnd(){
		PlayerControllerSky.GameEnd ();
	}

	IEnumerator UpdateDrop(){
		float time = 0;
		while (true) {
			if (rig2D.velocity.y < -8) {
				time += Time.deltaTime;
				if (time > 3.5f) {
					Instantiate (ParticleManager.Instance.particle_playerDead, transform.position, transform.rotation);
					Camera.main.transform.DOShakePosition (0.4f, 1, 10, 90, false, true);
					GameOverPre ();
					yield break;
				}
			} else {
				time = 0;
			}
			yield return null;
		}
	}

	void GameWin(){
		throwHook.isStart = false;
		PlayerPrefs.SetInt("Rank", ProgressSlider.Instance.GetRankNumber ());

		//获取排名倍数
		int rank = PlayerPrefs.GetInt ("Rank", 1);
		int mut = 1;
		if (rank == 1) {
			mut = 10;
		} else if (rank == 2) {
			mut = 5;
		} else if (rank == 3) {
			mut = 2;
		} else {
			mut = 1;
		}

		int goldSum = ProgressSlider.Instance.goldSum;
		PlayerPrefs.SetInt ("CurGold", goldSum );
		PlayerPrefs.SetInt ("goldMut", mut);


		progress.SetActive (false);
		rankPop.SetActive (true);
		if (throwHook.hookTarget) {
			throwHook.hookTarget.Find ("RocketCollider").GetComponentInChildren<PolygonCollider2D> ().enabled = false;
		}
	//	Invoke ("GameEnd", 0.4f);
	}
}
