using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Together;

public class OfflinePop : MonoBehaviour {
	public Text collect;
	public Text collectDouble;
	public Text gold;
	public Button doubleBtn;
	public FlyGold flyGold;
	public Skill skill;

	int moneyEarn = 0;

	void OnEnable(){
		UpdateSettleState ();
	}

	void UpdateSettleState(){
		int offlineMin = PlayerPrefs.GetInt ("OfflineMin",0);
		int moneyLvl = PlayerPrefs.GetInt ("moneyEarningValue", 10);
		moneyEarn = offlineMin * moneyLvl;
		collect.text = "$" + Conversion.UnitChange (moneyEarn);
		gold.text = collect.text;
		collectDouble.text = "$" + Conversion.UnitChange (moneyEarn * 2);
		CheckButton ();
	}

	void CheckButton(){
		doubleBtn.interactable = false;
		if (TGSDK.CouldShowAd (TZ_TGSDK.offlineID)) {
			doubleBtn.interactable = true;
		}
	}

	public void OnCollectBtn(){
		PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt("gold",0) + moneyEarn );
		gameObject.SetActive (false);
		flyGold.FlyGoldGenerate ();
		skill.UpdateSkillState ();
	}

	public void OnDoubleBtn(){
		TGSDK.ShowAd (TZ_TGSDK.doubleID);

		TGSDK.AdCloseCallback = (string obj) => {
			PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt ("gold", 0) + moneyEarn * 2);
			gameObject.SetActive (false);
			flyGold.FlyGoldGenerate ();
			skill.UpdateSkillState ();
		};
		TGSDK.AdRewardFailedCallback = (string obj) => {
			OnCollectBtn();
		};
		TGSDK.AdShowFailedCallback = (string obj) => {
			OnCollectBtn();
		};
	}
}
