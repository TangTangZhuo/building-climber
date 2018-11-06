using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum GameState{
	isShooting,
	isHooking,
	isTakeBacking,
	isInSky
}

public class ThrowHook : MonoBehaviour {
	public GameObject hook;

	public Transform ragDoll;

	[HideInInspector]
	public GameObject curHook;
	[HideInInspector]
	public GameObject curRocket;
	Vector3 endDirection = Vector3.zero;

	[HideInInspector]
	public RopeSriptes ropeSriptes;
	[HideInInspector]
	public Transform hookTarget;
	public GameObject drawCircle;
	GameObject drawCircleObj;
	public float hookTargerSpeed = 10;

	[HideInInspector]
	public float radius = 1;
	[HideInInspector]
	public float radiusChange = 0.1f;

	public GameState gameState = GameState.isInSky;
	public float endPower = 1000;

	Vector3 mousePos;

	Rigidbody2D rig2D;

	public bool isStart = false;

	bool isOverPunch = false;

	Camera mainCamera;

	void Awake(){
		
	}

	void Start () {
		//curRocket = null;
		radius = 3;
		radiusChange = 0.1f;

		rig2D = GetComponent<Rigidbody2D> ();

		StartCoroutine ("PunchTrans");

		PlayerPrefs.SetFloat ("maxSpeedValue", 7);

		mainCamera = Camera.main;
	}
		


	// Update is called once per frame
	void Update () {
		if (rig2D.velocity.y < -10) {
			rig2D.velocity = new Vector2 (rig2D.velocity.x, -10);
		}

		if (isStart) {

			if (!isOverPunch) {
				OverPunch ();
			}

			if (gameState == GameState.isInSky) {

				if (mainCamera.orthographicSize == 6f) {
					mainCamera.DOOrthoSize (7.5f, 0.5f / Time.timeScale);
				}

				if (Input.GetMouseButtonDown (0)) {
					if (curRocket) {
						
						gameState = GameState.isShooting;
						//Vector2 destiny = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						Vector2 destiny = (Vector2)curRocket.transform.position;
						curHook = (GameObject)Instantiate (hook, transform.position, Quaternion.LookRotation (Vector3.forward, destiny - (Vector2)transform.position));
						curHook.GetComponent<RopeSriptes> ().destiny = destiny;
						ropeSriptes = curHook.GetComponent<RopeSriptes> ();
						if (drawCircleObj) {
							StopCoroutine ("CircleFollowPlayer");
							Destroy (drawCircleObj);

						}
					}
				}
			}
			if (gameState == GameState.isHooking) {

				if (mainCamera.orthographicSize == 7.5f) {
					mainCamera.DOOrthoSize (6, 0.5f / Time.timeScale);
				}

				if (Input.GetMouseButtonUp (0)) {
					gameState = GameState.isTakeBacking;
					hookTarget.DORotate (new Vector3 (0, 0, 0), 0.5f, RotateMode.Fast);
					ShootPlayer ();
				}
					
			}
			if (gameState == GameState.isHooking) {
				if (Input.GetMouseButton (0)) {
					if (hookTarget) {
						float mouseDetal = mousePos.x - Camera.main.ScreenToViewportPoint (Input.mousePosition).x;
						FlyController flyController = hookTarget.GetComponent<FlyController> ();
						if (flyController.speed > PlayerPrefs.GetFloat ("maxSpeedValue", 5)) {
							flyController.speed -= Time.deltaTime * 8;
						}
//						if (flyController.speed <= 10&&flyController.speed>1) {
//							flyController.speed -= Time.deltaTime * 1;
//						}
						if (mouseDetal > 0.002f) {
							hookTarget.position = Vector3.Lerp (hookTarget.position, hookTarget.position + Vector3.left, hookTargerSpeed * Time.deltaTime);
							hookTarget.DORotate (new Vector3 (-30, 30, 30), 0.3f, RotateMode.Fast);
						} else if (mouseDetal < -0.002f) {
							hookTarget.position -= Vector3.left * hookTargerSpeed * Time.deltaTime;
							hookTarget.DORotate (new Vector3 (-30, -30, -30), 0.3f, RotateMode.Fast);
						} else {
							hookTarget.DORotate (new Vector3 (0, 0, 0), 0.6f, RotateMode.Fast);
						}
					}
				}
			}
			mousePos = Camera.main.ScreenToViewportPoint (Input.mousePosition);
		}
	}

	//准备阶段晃动
	IEnumerator PunchTrans(){
		while (!isStart) {
			transform.DOShakePosition (100, 0.1f, 1, 90, false,false);
			yield return new WaitForSeconds (100);
		}
	}

	void OverPunch(){
		StopCoroutine ("PunchTrans");
		transform.DOKill (false);
		isOverPunch = true;
	}

	void ShootPlayer(){
		RopeSriptes m_RopeSripts = curHook.GetComponent<RopeSriptes> ();
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
				if (hookTarget) {
					endDirection = new Vector3 (-Mathf.Tan (hookTarget.rotation.eulerAngles.z / 180f * Mathf.PI), 1);
				} else {
					endDirection = Vector3.up;				
				}
			}
			while (Vector3.Distance (player.position, nodes [i].transform.position) > 0.1f) {
				
				player.position = Vector3.MoveTowards (player.position, nodes [i].transform.position, 10*Time.deltaTime);

				yield return null;
			}
			if (i > 1) {
				player.position = Vector3.MoveTowards (player.position, nodes [i - 1].transform.position, 10 * Time.deltaTime);
			}
			if (ropeSriptes.vertexCount > 0)
				ropeSriptes.vertexCount--;
			yield return null;
		}
		if (curHook) {
			Destroy (curHook);
			gameState = GameState.isInSky;
			GetComponent<Rigidbody2D> ().AddForce (endDirection.normalized*endPower);
			DestoryRocket ();
			RocketColorManager.Instance.color1.SetFloat ("_ThresholdY", -8.5f);
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

		if (radius > 1) {
			radius -= radiusChange;
		}
		drawCircleObj.GetComponent<CircleCollider2D> ().offset = Vector2.zero;
		drawCircleObj.GetComponent<CircleCollider2D> ().radius = radius;
		DrawCircle.ToDrawCircle (drawCircleObj.transform, Vector3.zero, radius);
		StartCoroutine ("CircleFollowPlayer");
	}

	public void StartGame(){
		GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
		Invoke ("GenerateCircle", 0.2f);
		//GenerateCircle ();
		isStart = true;
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


	public IEnumerator ChangeRocketColor(Transform rocket){
		MeshRenderer rightFootMat = rocket.Find ("rocket3D").Find ("RightFoot").GetComponent<MeshRenderer>();
		MeshRenderer LeftFootMat = rocket.Find ("rocket3D").Find ("LeftFoot").GetComponent<MeshRenderer>();
		MeshRenderer DownMat = rocket.Find ("rocket3D").Find ("Down").GetComponent<MeshRenderer>();


		Material[] materials = new Material[]{ RocketColorManager.Instance.normal, RocketColorManager.Instance.color1 };
		rightFootMat.materials = materials;
		LeftFootMat.materials = materials;
		DownMat.materials = materials;

//		float thresholdY = -8.5f;
//		while (thresholdY > -7.1f) {
//			thresholdY -= Time.deltaTime;
//			rightFootMat.materials[1].SetFloat ("_ThresholdY", thresholdY);
//			LeftFootMat.materials[1].SetFloat ("_ThresholdY", thresholdY);
			yield return null;
//		}
	}

}
