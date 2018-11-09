using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Together;
using DG.Tweening;

public class TurnTable : MonoBehaviour {

	//需要配合Yomob广告SDK,DGSDK使用

	//获取转盘旋转脚本
	public Rotation rotation;
	//获取指针检测脚本
	public Needle needle;
	//获取转转盘所需金币
	public Text goldText;
	//获取转盘倍数显示
	public Text multiText;
	//获取转盘元素集合
	public Transform turnTable;
	//获取按钮
	public GameObject turnBtn;
	public GameObject goldBtn;
	public GameObject backBtn;

	public FlyGold flyTreasure;

	//本局获得金币数
	int curGold = 0;
	//所持有的金币数
	int holdGold = 0;

	//转盘是否结束旋转
	bool isFinish = false;


	//转盘元素倍数
	float[] multiple = new float[]{1.5f,0.5f,1,1.25f,0.75f,1};
	//转盘元素金币数
	int[] golds;
	//宝箱数量
	int treasureNum = 0;
	//宝箱数量UI显示
	int treasureText = 0;

	//是否再次看过广告
	bool isWatchAD = false;
	//是否观看广告
	bool isFirstAD = false;

	//单例
	static TurnTable instance;
	public static TurnTable Instance{
		get{ return instance;}
	}
	void Awake(){
		instance = this;
	}

	//初始化
	void OnEnable () {
		curGold = PlayerPrefs.GetInt ("CurGold", 0);
		holdGold = PlayerPrefs.GetInt ("gold", 0);
		goldText.text = Conversion.UnitChange(curGold);
		treasureNum = ProgressSlider.Instance.treasureNum;
		//treasureNum = 3;
		multiText.text = "×" + 0;

		rotation.RotationFinish +=()=>{ 
			RotateFinish ();
		};
		needle.CheckNeedleCallBack += (Collider2D coll) => {
			CheckNeedle(coll);
		};
			
		InitTable (curGold);

		flyTreasure.FlyGoldGenerate (treasureNum);
	}

	public void AddMultiText(){
		multiText.text = "×" + (++treasureText);
		print (treasureText);
	}

	//更新转盘元素
	void InitTable(int gold){
		golds = new int[multiple.Length];
		for (int i = 0; i < multiple.Length; i++) {
			turnTable.GetChild (i).Find ("Text").GetComponent<Text> ().text = ((int)(gold * multiple [i])).ToString();
			golds [i] = (int)(gold * multiple [i]);
		}
	}

	//点击广告旋转按钮
	public void OnTurnBtn(){
		if (TGSDK.CouldShowAd (TZ_TGSDK.turnTableID)) {
			
			if (!isWatchAD) {
				TGSDK.ShowAd (TZ_TGSDK.turnTableID);
			} else {
				if (TGSDK.CouldShowAd (TZ_TGSDK.turnTableID)) {
					TGSDK.ShowAd (TZ_TGSDK.turnAgainID);
				} else {
					TipPop.GenerateTip ("no ads", 0.5f);
				}
			}

			HideBtn ();


//			TGSDK.AdCompleteCallback = (string obj) => {				
//				rotation.RotateThis();
//				isFirstAD = true;
//			};
			TGSDK.AdCloseCallback = (string obj) => {
				PlayerPrefs.SetInt("flyGold",1);
				rotation.RotateThis();

					
				
				isFirstAD = true;


			};
			TGSDK.AdRewardFailedCallback = (string obj) => {
				OnBackBtn();
			};
			TGSDK.AdShowFailedCallback = (string obj) => {
				OnBackBtn();
			};

		} else {
			TipPop.GenerateTip ("no ads", 0.5f);
		}

	}

	//点击金币旋转按钮
	public void OnGoldBtn(){
		if (holdGold >= curGold) {
			PlayerPrefs.SetInt ("gold", holdGold - curGold);
			PlayerPrefs.SetInt("flyGold",1);
			rotation.RotateThis ();
			HideBtn ();
			isWatchAD = true;

		} else {
			TipPop.GenerateTip ("not enough money", 0.5f);
		}
	}

	//点击返回按钮
	public void OnBackBtn(){
		gameObject.SetActive (false);
		HideBtn ();
		PlayerControllerSky.GameEnd ();
	}

	//转盘结束回调
	void RotateFinish(){
		isFinish = true;
	}

	//转盘结束时获得对应奖励
	void CheckNeedle(Collider2D coll){
		if (isFinish) {
			if (coll.name.StartsWith ("item")) {
				int index = int.Parse(coll.name.Split (new char[]{ 'm' }) [1]);
				PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt ("gold", 0) + golds [index]*treasureNum);
				isFinish = false;

				//看过广告更新UI信息
				UpdateUIState ();

				//第一次看广告不处理，第二次看广告退出转盘
				if (isWatchAD) {
					Invoke ("OnBackBtn", 0.5f);
				}
				//isWatchAD = true;

			}else{
				rotation.RotateLittle ();
			}

		}
	}

	//旋转时隐藏按钮
	void HideBtn(){
		backBtn.SetActive (false);
		goldBtn.SetActive (false);
		turnBtn.SetActive (false);
	}

	//看完广告后更新UI
	void UpdateUIState(){
		if (isFirstAD) {
			if (!isWatchAD) {
				backBtn.transform.position = goldBtn.transform.position;
				backBtn.SetActive (true);
				turnBtn.SetActive (true);
				turnBtn.GetComponentInChildren<Text> ().text = "Turn AGAIN";
			}
		}
	}


	//再次旋转
	public void TurnAgain(){
		
	}
}
