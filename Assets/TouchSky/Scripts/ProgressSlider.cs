using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour {
	[HideInInspector]
	public Slider slider;
	[HideInInspector]
	public Text ranking;

	public Text curGold;

	public Text treasure;
	[HideInInspector]
	public int treasureNum = 0;

	public int goldSum = 0;

	Transform player;

	GameObject[] AIs;

	StartButton startButton;

	static ProgressSlider instance;
	public static ProgressSlider Instance{
		get{ return instance;}
	}
	void Awake(){
		instance = this;	
	}

	// Use this for initialization
	void Start () {
		startButton = StartButton.Instance;
	}

	void OnEnable(){
		slider = GetComponent<Slider> ();
		ranking = transform.Find("Ranking").GetComponent<Text> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		AIs = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<RocketGenerate> ().AIs;
		float distance = 350 + PlayerPrefs.GetInt ("curLevel", 1) * 2;
		slider.maxValue = distance;
		slider.minValue = GameObject.FindGameObjectWithTag ("Player").transform.position.y;
		curGold.text = "$" + 0;
	}

	// Update is called once per frame
	void Update () {
		if (startButton.isStart) {
			slider.value = player.position.y;
			StartCoroutine (GetRank());

		}
	}

	IEnumerator GetRank(){
		while(startButton.isStart){			
			ranking.text = GetRankNumber ()+"TH";
			yield return new WaitForSeconds (0.1f);
		}
	}

	public int GetRankNumber(){
		int index = 1;
		for (int i = 0; i < AIs.Length; i++) {
			if (AIs [i].transform.position.y > player.position.y) {
				index++;
			}
		}
		return index;
	}

	public void AddTreasure(int number){
		treasureNum+=number;
		treasure.text = treasureNum.ToString ();
	}

}
