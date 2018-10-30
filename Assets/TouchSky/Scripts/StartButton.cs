using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

	public GameObject Terminal;
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
		distance = 650;
		ProgressSlider.Instance.slider.maxValue = distance;
		ProgressSlider.Instance.slider.minValue = GameObject.FindGameObjectWithTag ("Player").transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame(){
		isStart = true;
		GetComponent<Button> ().gameObject.SetActive (false);
		Instantiate (Terminal, new Vector3 (0, distance, 0), Terminal.transform.rotation);

	}

}
