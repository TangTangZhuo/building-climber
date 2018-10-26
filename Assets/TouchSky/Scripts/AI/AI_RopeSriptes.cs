using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_RopeSriptes : MonoBehaviour {
	public Vector2 destiny;
	public float speed = 1;

	public float distance = 2;

	public GameObject nodePrefab;
	[HideInInspector]
	public GameObject player;
	[HideInInspector]
	public GameObject lastNode;
	public string ainame = "";

	public int vertexCount = 2;

	public List<GameObject> nodes = new List<GameObject> ();

	bool done = false;
	[HideInInspector]
	public LineRenderer lr;


	public AI_ThrowHook throwHook;

	void OnEnable () {
		lr = GetComponent<LineRenderer> ();

		player = GameObject.Find (ainame);
		throwHook = player.GetComponent<AI_ThrowHook> ();
		lastNode = transform.gameObject;
		nodes.Add (transform.gameObject);
	}


	void Update () {
		if (!done) {
			transform.position = Vector2.MoveTowards (transform.position, destiny, speed);

			if ((Vector2)transform.position != destiny) {
				if (Vector2.Distance (player.transform.position, lastNode.transform.position) > distance) {
					CreatNode ();
				}
			} else if (!done) {
				done = true;
				while (Vector2.Distance (player.transform.position, lastNode.transform.position) > distance) {
					CreatNode ();
				}
				HingeJoint2D joint = lastNode.GetComponent<HingeJoint2D> ();
				if (joint)
					joint.connectedBody = player.GetComponent<Rigidbody2D> ();
				GetComponent<DistanceJoint2D> ().connectedBody = player.GetComponent<Rigidbody2D> ();
			}
		}
		if (throwHook.gameState != AI_ThrowHook.AIState.isInSky) {
			RenderLine ();
		}
		if (throwHook.gameState == AI_ThrowHook.AIState.isInSky) {

		}
	}



	void RenderLine(){
		lr.positionCount = vertexCount;
		int i ;
		for (i = 0; i < vertexCount-1; i++) {
			lr.SetPosition (i, nodes [i].transform.position);
		}
		lr.SetPosition (i, player.transform.position);
	}

	void CreatNode(){
		Vector2 pos2Create = player.transform.position - lastNode.transform.position;
		pos2Create.Normalize ();
		pos2Create *= distance;
		pos2Create += (Vector2)lastNode.transform.position;

		GameObject go = (GameObject)Instantiate (nodePrefab, pos2Create, Quaternion.identity);

		go.transform.SetParent (transform);

		if (vertexCount == 2) {
			go.AddComponent<HingeJoint2D> ();
			HingeJoint2D[] joints = go.GetComponents<HingeJoint2D> ();
			joints [1].connectedBody = lastNode.GetComponent<Rigidbody2D> ();
		} else {
			HingeJoint2D joint = lastNode.GetComponent<HingeJoint2D> ();
			joint.connectedBody = go.GetComponent<Rigidbody2D> ();
		}

		lastNode = go;

		nodes.Add (lastNode);

		vertexCount++;
	}
}
