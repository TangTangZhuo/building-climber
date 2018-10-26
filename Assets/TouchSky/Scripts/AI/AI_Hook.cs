﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AI_Hook : MonoBehaviour {
	Rigidbody2D hookRig2D;
	AI_RopeSriptes ropeScritpes;

	TargetJoint2D m_TargetJoint;
	public Transform target;

	public float speed = 1f;

	// Use this for initialization
	void Start () {
		hookRig2D = GetComponent<Rigidbody2D> ();
		ropeScritpes = GetComponent<AI_RopeSriptes> ();
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
				AI_ThrowHook ai_ThrowHook = ropeScritpes.player.GetComponent<AI_ThrowHook> () ;
				ai_ThrowHook.gameState = AI_ThrowHook.AIState.isHooking;
				target = obj.parent;
				//target.transform.DOPunchPosition (transform.position-target.position, 0.5f, 1, 1, false);
				target.GetComponent<FlyController> ().speed = 20;
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

}