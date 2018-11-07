using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Hook : MonoBehaviour {
	Rigidbody2D hookRig2D;
	RopeSriptes ropeScritpes;
	ProgressSlider progress;
	ThrowHook throwHook;

	TargetJoint2D m_TargetJoint;
	public Transform target;

	public float speed = 1f;

	public GameObject addGoldText;

	int gold = 0;
	//int goldSum = 0;
	// Use this for initialization
	void Start () {
		hookRig2D = GetComponent<Rigidbody2D> ();
		ropeScritpes = GetComponent<RopeSriptes> ();
		progress = ProgressSlider.Instance;
		throwHook = ropeScritpes.player.GetComponent<ThrowHook> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (m_TargetJoint) {
			if (target) {
				m_TargetJoint.target = target.position;
			}
			//target.position = transform.position;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (transform.parent == null) {
			Transform obj = null;
			if (coll.transform.parent != null) {
				obj = coll.transform.parent;
			}
			if (obj && obj.name == "RocketCollider") {
				if (m_TargetJoint)
					return;
				ropeScritpes.player.GetComponent<ThrowHook> ().gameState = GameState.isHooking;
				target = obj.parent;

				target.Find ("sprinting").gameObject.SetActive (true);
				coll.GetComponent<PolygonCollider2D> ().enabled = false;
				StartCoroutine (ResetRocket (target, coll, (PlayerPrefs.GetFloat ("speedUpValue", 15)-PlayerPrefs.GetFloat ("maxSpeedValue", 7))/8f));

				MultiHaptic.HapticHeavy ();
				MultiHaptic.HapticMedium ();
				MultiHaptic.HapticLight ();

				AddGold ();

				StartCoroutine (throwHook.ChangeRocketColor (target));

				target.Find ("Rocket").gameObject.SetActive (true);

				//target.transform.DOPunchPosition (transform.position-target.position, 0.5f, 1, 1, false);
				target.GetComponent<FlyController> ().speed = PlayerPrefs.GetFloat ("speedUpValue", 15);
				coll.tag = "curRocket";
				Instantiate (ParticleManager.Instance.particle_hooking, transform.position, transform.rotation).transform.parent = transform;
				ropeScritpes.throwHook.hookTarget = target;
				//target.position = transform.position;
				target.position += (transform.position - target.position).normalized;
				Camera.main.transform.DOShakePosition (0.2f, 0.5f, 10, 90, false, true);
				m_TargetJoint = gameObject.AddComponent<TargetJoint2D>();
				m_TargetJoint.dampingRatio = 1;
				m_TargetJoint.frequency = 15;
				m_TargetJoint.anchor = m_TargetJoint.transform.InverseTransformPoint (transform.position);
			}
		}
	}

	IEnumerator ResetRocket(Transform target,Collider2D coll,float time){
		yield return new WaitForSeconds (time);
		if (target) {
			target.Find ("sprinting").gameObject.SetActive (false);
			coll.GetComponent<PolygonCollider2D> ().enabled = true;
		}
	}

	void AddGold(){
		gold = PlayerPrefs.GetInt ("moneyEarningValue", 10);
		StartCoroutine (GenerateGold (5,gold,1));
	}

	IEnumerator GenerateGold(int num,int gold,float time){
		while (num > 0) {
			//Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
			Vector3 pos = transform.position;
			GameObject go = Instantiate (addGoldText, pos, Quaternion.identity,GameObject.Find("Canvas").transform);
			Text goText = go.GetComponent<Text> ();
			goText.text = "$"+Conversion.UnitChange(gold);
			goText.DOFade (0.5f, time).OnComplete(()=>{
				Destroy(go);
			});
			goText.transform.DOLocalMove (transform.position+new Vector3(0,-500,0), time, false);

			progress.goldSum += gold;
			progress.curGold.text ="$" + Conversion.UnitChange (progress.goldSum);
			num--;
			yield return new WaitForSeconds (0.1f);
		}
	}
		


}
