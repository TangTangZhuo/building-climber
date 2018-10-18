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

	GameObject curHook;
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

	public GameState gameState = GameState.isInSky;
	public float endPower = 1000;

	Vector3 mousePos;

	bool isStart = false;
	void Awake(){
		
	}

	void Start () {
		//curRocket = null;

	}
		

	// Update is called once per frame
	void Update () {
		if (isStart) {
			if (gameState == GameState.isInSky) {
				//if(Input.GetTouch)
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
			if (gameState == GameState.isHooking)
		//if (Input.GetKeyUp (KeyCode.A)) {
		if (Input.GetMouseButtonUp (0)) {
				gameState = GameState.isTakeBacking;
				hookTarget.DORotate (new Vector3 (0, 0, 0), 0.2f, RotateMode.Fast);
				ShootPlayer ();
			}
			if (gameState == GameState.isHooking) {
				if (Input.GetMouseButton (0)) {
					if (hookTarget) {
						float mouseDetal = mousePos.x - Camera.main.ScreenToViewportPoint (Input.mousePosition).x;

						if (mouseDetal > 0.001f) {
							hookTarget.position += Vector3.left * hookTargerSpeed * Time.deltaTime;
							hookTarget.DORotate (new Vector3 (0, 0, 30), 0.2f, RotateMode.Fast);
						} else if (mouseDetal < -0.001f) {
							hookTarget.position -= Vector3.left * hookTargerSpeed * Time.deltaTime;
							hookTarget.DORotate (new Vector3 (0, 0, -30), 0.2f, RotateMode.Fast);
						} else {
							hookTarget.DORotate (new Vector3 (0, 0, 0), 0.2f, RotateMode.Fast);
						}
					}
				}
			}
			mousePos = Camera.main.ScreenToViewportPoint (Input.mousePosition);
		}
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
			if (i == 0) {
				endDirection = nodes [i].transform.position - player.position;
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
			GenerateCircle ();
		}

	}

	void DestoryRocket(){
		Destroy (hookTarget.gameObject);
	}

	public void GenerateCircle(){
		drawCircleObj = Instantiate (drawCircle);
		radius = 2;
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

}
