using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketColorManager : MonoBehaviour {

	public Material color1;
	public Material color2;
	public Material color3;
	public Material color4;
	public Material color5;
	public Material color6;
	public Material normal;

	static RocketColorManager instance;
	public static RocketColorManager Instance{
		get{ return instance;}
	}
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
