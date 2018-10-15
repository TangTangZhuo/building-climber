using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSriptes : MonoBehaviour {
	public Vector2 destiny;
	public float speed = 1;
//	public float keepSpeed = 2;

	public float distance = 2;

	public GameObject nodePrefab;
	[HideInInspector]
	public GameObject player;
	[HideInInspector]
	public GameObject lastNode;


	int vertexCount = 2;

	public List<GameObject> nodes = new List<GameObject> ();

	bool done = false;
	LineRenderer lr;
	Transform nodesTrans;
	// Use this for initialization
	void Start () {
		lr = GetComponent<LineRenderer> ();

		player = GameObject.FindGameObjectWithTag ("Player");
		lastNode = transform.gameObject;
		nodes.Add (transform.gameObject);
		nodesTrans = new GameObject ("Nodes").transform;
	}
	
	// Update is called once per frame
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
				lastNode.GetComponent<HingeJoint2D> ().connectedBody = player.GetComponent<Rigidbody2D> ();
				int childCount = transform.childCount;
				for (int i = 0; i < childCount; i++) {
					Transform child = transform.GetChild (0);
					child.GetComponent<HingeJoint2D> ().autoConfigureConnectedAnchor = false;
					child.SetParent (nodesTrans);
				}
				transform.GetComponent<HingeJoint2D> ().autoConfigureConnectedAnchor = false;
			}
		}
		RenderLine ();

		//KeepDistance (nodes);
	}

	void KeepDistance(List<GameObject> nodeObj){
		for (int i = 0; i < nodeObj.Count-1; i++) {
			if (Vector3.Distance (nodeObj [i].transform.position, nodeObj [i+1].transform.position)>distance) {
				nodeObj [i + 1].transform.position = Vector3.Lerp (nodeObj [i + 1].transform.position, nodeObj [i].transform.position,   Time.deltaTime);
			}
		}
		//print (Vector3.Distance (nodeObj [0].transform.position, nodeObj [1].transform.position));
	}

	void RenderLine(){
		lr.positionCount = vertexCount;
		int i ;
		for (i = 0; i < nodes.Count; i++) {
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

		lastNode.GetComponent<HingeJoint2D> ().connectedBody = go.GetComponent<Rigidbody2D> ();

		lastNode = go;

		nodes.Add (lastNode);

		vertexCount++;
	}
}
