using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class AI_ThrowHook : MonoBehaviour {

	public enum AIState{
		isShooting,
		isHooking,
		isTakeBacking,
		isInSky
	}

	//钩子预制体
	public GameObject hook;
	//钩子实例
	GameObject curHook;

	[HideInInspector]
	//瞄准圈内即将勾住的飞船
	public GameObject curRocket;
	//发射方向
	Vector3 endDirection = Vector3.zero;

	[HideInInspector]
	//钩爪上生成绳子脚本
	public AI_RopeSriptes ai_ropeSriptes;
	[HideInInspector]
	//当前勾住的飞船
	public Transform hookTarget;
	//瞄准镜预制体
	public GameObject drawCircle;
	//瞄准镜实例
	GameObject drawCircleObj;
	//勾住飞船的平移速度
	public float hookTargerSpeed = 10;

	[HideInInspector]
	//瞄准镜半径
	public float radius = 1;
	[HideInInspector]
	//瞄准镜缩放间隔
	public float radiusChange = 0.1f;

	//AI状态
	public AIState gameState = AIState.isInSky;
	//脱离飞船时的弹射力度
	public float endPower = 1000;

	Vector3 mousePos;

	bool isStart = false;
	//是否开始游戏
	bool isInit = false;

	//飞船变向开关
	int targetDirChange = 3;
	//飞船变向频率
	float targetDirChangeTime = 0;

	void Awake(){

	}

	void Start () {
		radius = 3;
		radiusChange = 0.1f;
	}


	// Update is called once per frame
	void Update () {
		StartGame ();
		if (!hookTarget) {
			gameState = AIState.isInSky;
		}
		if (hookTarget) {
			hookTarget.position = new Vector3 (Mathf.Clamp (hookTarget.position.x, -9, 10), hookTarget.position.y, hookTarget.position.z);
			if (hookTarget.position.x < -8.5f) {
				targetDirChange = 2;
			}if (hookTarget.position.x > 9.5f) {
				targetDirChange = 1;
			}
		} else {
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -9, 10), transform.position.y, transform.position.z);

		}
		if (isStart) {			
			if (gameState == AIState.isInSky) {				
				if (curRocket) {
					gameState = AIState.isShooting;
					Vector2 destiny = (Vector2)curRocket.transform.position;
					curHook = (GameObject)Instantiate (hook, transform.position, Quaternion.LookRotation (Vector3.forward, destiny - (Vector2)transform.position));
					curHook.GetComponent<AI_RopeSriptes> ().destiny = destiny;
					curHook.GetComponent<AI_RopeSriptes> ().ainame = transform.name;
					curHook.SetActive (true);
					ai_ropeSriptes = curHook.GetComponent<AI_RopeSriptes> ();
					if (drawCircleObj) {
						StopCoroutine ("CircleFollowPlayer");
						Destroy (drawCircleObj);
					}
				}				
			}

			if (gameState == AIState.isHooking) {
				if (hookTarget) {
					Vector2 direction = new Vector3 (-Mathf.Tan (hookTarget.rotation.eulerAngles.z / 180f * Mathf.PI), 1);
					float length = 10;
					RaycastHit2D[] hit2D = Physics2D.RaycastAll ((Vector2)curHook.transform.position, direction, length);

					for (int i = 0; i < hit2D.Length; i++) {
						if (hit2D [i].collider.tag == "rocket") {
							gameState = AIState.isTakeBacking;
							hookTarget.DORotate (new Vector3 (0, 0, 0), 1f, RotateMode.Fast);
							ShootPlayer ();
							break;
						}
					}
				}
			}
			if (gameState == AIState.isHooking) {				
				if (hookTarget) {						
					FlyController flyController = hookTarget.GetComponent<FlyController> ();
					if (flyController.speed > 10) {
						flyController.speed -= Time.deltaTime * 8;
					}
					if (flyController.speed <= 10 && flyController.speed > 1) {
						flyController.speed -= Time.deltaTime * 1;
					}
					if (targetDirChange == 1) {
						hookTarget.position = Vector3.Lerp (hookTarget.position, hookTarget.position + Vector3.left, hookTargerSpeed/3 * Time.deltaTime);
						hookTarget.DORotate (new Vector3 (0, 0, 30), 0.3f, RotateMode.Fast);		
						targetDirChangeTime += Time.deltaTime;
						if (targetDirChangeTime > 2) {
							targetDirChangeTime = 0;
							targetDirChange = Random.Range (1, 4);
						}
					}
					if (targetDirChange == 2) {
						hookTarget.position -= Vector3.left * hookTargerSpeed/3 * Time.deltaTime;
						hookTarget.DORotate (new Vector3 (0, 0, -30), 0.3f, RotateMode.Fast);	
						targetDirChangeTime += Time.deltaTime;
						if (targetDirChangeTime > 2) {
							targetDirChangeTime = 0;
							targetDirChange = Random.Range (1, 4);

						}
					}
					if (targetDirChange == 3) {
						hookTarget.DORotate (new Vector3 (0, 0, 0), 0.6f, RotateMode.Fast);	
						targetDirChangeTime += Time.deltaTime;
						if (targetDirChangeTime > 2) {
							targetDirChangeTime = 0;
							targetDirChange = Random.Range (1, 4);
						}
					}
						
				}
			} 

		}
	}

	void ShootPlayer(){
		AI_RopeSriptes m_RopeSripts = curHook.GetComponent<AI_RopeSriptes> ();
		List<GameObject> nodes = m_RopeSripts.nodes;
		Transform player = m_RopeSripts.player.transform;
		LineRenderer lr = m_RopeSripts.lr;
		StartCoroutine(TakeBackRope (nodes, player,lr));
	}



	IEnumerator TakeBackRope(List<GameObject> nodes,Transform player,LineRenderer lr){
		int lrCount = lr.positionCount;

		for (int i = nodes.Count-1; i >= 0; i--) {
			Destroy (nodes [i].GetComponent<CircleCollider2D> ());
			lr.positionCount--;
			if (i == nodes.Count-1) {
				//endDirection = nodes [0].transform.position - player.position;
				endDirection = new Vector3(-Mathf.Tan(hookTarget.rotation.eulerAngles.z/180f*Mathf.PI),1);
			}
			while (Vector3.Distance (player.position, nodes [i].transform.position) > 0.1f) {

				player.position = Vector3.MoveTowards (player.position, nodes [i].transform.position, 10*Time.deltaTime);

				yield return null;
			}
			if (i > 1) {
				player.position = Vector3.MoveTowards (player.position, nodes [i - 1].transform.position, 10 * Time.deltaTime);
			}
			if (ai_ropeSriptes.vertexCount > 0)
				ai_ropeSriptes.vertexCount--;
			yield return null;
		}
		if (curHook) {
			Destroy (curHook);
			gameState = AIState.isInSky;
			GetComponent<Rigidbody2D> ().AddForce (endDirection.normalized*endPower);
			DestoryRocket ();
			GenerateCircle ();
		}

	}

	void DestoryRocket(){
		if (hookTarget) {
			Destroy (hookTarget.gameObject);
		}
	}

	public void GenerateCircle(){
		drawCircleObj = Instantiate (drawCircle);

		drawCircleObj.GetComponent<AI_InCircle>().aiName = transform.name;
		drawCircleObj.SetActive (true);

		if (radius > 1) {
			radius -= radiusChange;
		}

		drawCircleObj.GetComponent<CircleCollider2D> ().offset = Vector2.zero;
		drawCircleObj.GetComponent<CircleCollider2D> ().radius = radius;
		DrawCircle.ToDrawCircle (drawCircleObj.transform, Vector3.zero, radius);
		StartCoroutine ("CircleFollowPlayer");
	}

	void StartGame(){
		if (!isStart) {
			isInit = GameObject.FindGameObjectWithTag ("Player").GetComponent<ThrowHook> ().isStart;
			if (isInit) {
				GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
				Invoke ("GenerateCircle", 0.2f);
				isStart = true;
				isInit = false;
			}
		}
	}

	IEnumerator CircleFollowPlayer(){

		while (true) {
			if (drawCircleObj) {
				//drawCircleObj.transform.position = transform.position + endDirection.normalized * (radius+0.4f);
				drawCircleObj.transform.position = transform.position;
				drawCircleObj.transform.Rotate (new Vector3 (0, 0, 1.5f));
			}
			yield return null;
		}
	}

}
