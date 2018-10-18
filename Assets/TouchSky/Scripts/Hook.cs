using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
	Rigidbody2D hookRig2D;
	RopeSriptes ropeScritpes;

	TargetJoint2D m_TargetJoint;
	public Transform target;

	public float speed = 1f;
	// Use this for initialization
	void Start () {
		hookRig2D = GetComponent<Rigidbody2D> ();
		ropeScritpes = GetComponent<RopeSriptes> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (m_TargetJoint) {
			m_TargetJoint.target = target.position;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (transform.parent == null) {
			Transform obj = null;
			if (coll.transform.parent != null) {
				obj = coll.transform.parent;
			}
			if (obj.name == "RocketCollider") {
				if (m_TargetJoint)
					return;
				ropeScritpes.player.GetComponent<ThrowHook> ().gameState = GameState.isHooking;
				target = obj.parent;
				target.GetComponent<FlyController> ().speed = 3;
				ropeScritpes.throwHook.hookTarget = target;
				//target.position = transform.position;
				m_TargetJoint = gameObject.AddComponent<TargetJoint2D>();
				m_TargetJoint.anchor = m_TargetJoint.transform.InverseTransformPoint (transform.position);
			}
		}
	}
		
}
