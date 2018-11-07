﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

	public GameObject Terminal;
	public Transform BGTrigger;
	public GameObject Progress;
	public GameObject Skill;

	[HideInInspector]
	public float distance = 0;
	[HideInInspector]
	public bool isStart = false;

	static StartButton instance;
	public static StartButton Instance{
		get{ return instance;}
	}

	void Awake(){
		instance = this;	
	}

	// Use this for initialization
	void Start () {
		//distance = 350 + PlayerPrefs.GetInt ("curLevel", 1) * 2;
		distance = 50;
		//ProgressSlider.Instance.slider.maxValue = distance;
		//ProgressSlider.Instance.slider.minValue = GameObject.FindGameObjectWithTag ("Player").transform.position.y;


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame(){
		isStart = true;
		gameObject.SetActive (false);
		Progress.SetActive (true);
		Skill.SetActive (false);
		Instantiate (Terminal, new Vector3 (0, distance, 0), Terminal.transform.rotation);
		Instantiate (BGTrigger, new Vector3 (0, distance/3, 0), Quaternion.identity);
		Instantiate (BGTrigger, new Vector3 (0, distance/3*2, 0), Quaternion.identity);
		Camera.main.GetComponent<FollowTarget> ().ChangeOffset (new Vector3 (0, 4, 0));
	}

}
