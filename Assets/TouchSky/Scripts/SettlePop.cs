using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Together;

public class SettlePop : MonoBehaviour {
	public Text collect;
	public Text collectDouble;
	public Text gold;
	public Button doubleBtn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){
		UpdateSettleState ();
	}

	void UpdateSettleState(){
		int curGold = PlayerPrefs.GetInt ("CurGold", 0)* PlayerPrefs.GetInt ("goldMut", 1);
		PlayerPrefs.SetInt ("CurGold", curGold);
		collect.text = "$" + Conversion.UnitChange (curGold);
		gold.text = collect.text;
		collectDouble.text = "$" + Conversion.UnitChange (curGold * 2);
		CheckButton ();
	}

	void CheckButton(){
		doubleBtn.interactable = false;
		if (TGSDK.CouldShowAd (TZ_TGSDK.doubleID)) {
			doubleBtn.interactable = true;
		}
	}

	public void OnCollectBtn(){
		PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt("gold",0) + PlayerPrefs.GetInt ("CurGold", 0) );
		PlayerControllerSky.GameEnd ();
	}

	public void OnDoubleBtn(){
		TGSDK.ShowAd (TZ_TGSDK.doubleID);
		TGSDK.AdCompleteCallback = (string obj) => {
			PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt ("gold", 0) + PlayerPrefs.GetInt ("CurGold", 0) * 2);
			PlayerControllerSky.GameEnd ();
		};
		TGSDK.AdCloseCallback = (string obj) => {
			OnCollectBtn();
		};
		TGSDK.AdRewardFailedCallback = (string obj) => {
			OnCollectBtn();
		};
		TGSDK.AdShowFailedCallback = (string obj) => {
			OnCollectBtn();
		};
	}
}
