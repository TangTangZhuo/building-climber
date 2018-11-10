using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritingCollider : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "deadcloud") {
			Instantiate (ParticleManager.Instance.particle_rocketDead, transform.position, transform.rotation).transform.parent = transform;
			Destroy (coll.gameObject);
		}
		if (coll.tag == "rocket") {
			Instantiate (ParticleManager.Instance.particle_rocketDead, transform.position, transform.rotation).transform.parent = transform;
			Destroy (coll.transform.parent.parent.gameObject);
		}
	}
}
