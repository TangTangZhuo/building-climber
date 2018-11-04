using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankPop : MonoBehaviour {

	public Text race;
	public Text[] names;
	public Text[] golds;
	public Image[] images;
	public Sprite youImage;
	public GameObject settlePop;

	List<string> nameList;

	static RankPop instance;
	public static RankPop Instance{
		get{ return instance;}
	}
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		//添加名字

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){
		nameList = new List<string> ();
		InitName ();
		UpdateRankState ();
	}

	//更新排名信息
	public void UpdateRankState(){
		//更新关卡数字
		race.text ="RACE" + PlayerPrefs.GetInt ("curLevel", 1);

		//更新玩家背景颜色
		int rank = PlayerPrefs.GetInt ("Rank", 1);
		images [rank - 1].sprite = youImage;

		//更新玩家名字和金币
		names[rank-1].text = "YOU";

		int curGold = PlayerPrefs.GetInt("CurGold",0);
		golds [rank - 1].text ="$" + Conversion.UnitChange(curGold);

		//更新AI名字和金币
		for(int i=0;i<names.Length;i++){
			if (i != rank - 1) {
				string name = nameList [Random.Range (0, nameList.Count)];
				names [i].text = name;
				golds [i].text ="$" + Conversion.UnitChange((int)(curGold * (Random.Range (0.5f, 1.5f))));
				nameList.Remove (name);
			}
		}

	}

	void InitName(){
		nameList.Add ("Jack007");
		nameList.Add ("Rokie43e");
		nameList.Add ("Dunesterd");
		nameList.Add ("Fantastic");
		nameList.Add ("Amile11");
	}

	public void OnRankButton(){
		settlePop.SetActive (true);
		//gameObject.SetActive (false);
	}
}
