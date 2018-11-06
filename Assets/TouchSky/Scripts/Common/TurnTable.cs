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
	//获取转盘元素集合
	public Transform turnTable;
	//获取按钮
	public GameObject turnBtn;
	public GameObject goldBtn;
	public GameObject backBtn;

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

	//初始化
	void OnEnable () {
		curGold = PlayerPrefs.GetInt ("CurGold", 0);
		holdGold = PlayerPrefs.GetInt ("gold", 0);
		goldText.text = Conversion.UnitChange(curGold);
		rotation.RotationFinish +=()=>{ 
			RotateFinish ();
		};
		needle.CheckNeedleCallBack += (Collider2D coll) => {
			CheckNeedle(coll);
		};
			
		InitTable (curGold);
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
			TGSDK.ShowAd (TZ_TGSDK.turnTableID);
			HideBtn ();


			TGSDK.AdCompleteCallback = (string obj) => {				
				rotation.RotateThis();
			};
			TGSDK.AdCloseCallback = (string obj) => {
				OnBackBtn();
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
			rotation.RotateThis ();
			HideBtn ();
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
				PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt ("gold", 0) + golds [index]);
				isFinish = false;

				OnBackBtn ();

			}else{
				rotation.RotateLittle ();
			}

		}
	}

	void HideBtn(){
		backBtn.SetActive (false);
		goldBtn.SetActive (false);
		turnBtn.SetActive (false);
	}
}
