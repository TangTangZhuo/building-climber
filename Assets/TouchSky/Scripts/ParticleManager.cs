using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {
	public GameObject particle_Gift;
	public GameObject particle_playerHit;
	public GameObject particle_rocketDead;
	public GameObject particle_playerDead;
	public GameObject particle_playerEle;

	public static ParticleManager Instance{
		get{ return instance;}
	}
	private static ParticleManager instance;

	void Awake(){
		instance = this;
	}
}
