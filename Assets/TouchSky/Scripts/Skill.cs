using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Skill : MonoBehaviour {

	public GameObject max;
	public GameObject speedUp;
	public GameObject earning;

	public Button maxBtn;
	public Button speedUpBtn;
	public Button earningBtn;

	public Button maxBuyBtn;
	public Button speedUpBuyBtn;
	public Button earningBuyBtn;

	public Text gold;

	public Text maxSpeedLvl;
	public Text speedUpLvl;
	public Text moneyEarningLvl;

	public Text maxSpeedValue;
	public Text speedUpValue;
	public Text moneyEarningValue;

	public Text maxSpeedPercentage;
	public Text speedUpPercentage;
	public Text moneyEarningPercentage;

	public Text maxSpeedPrice;
	public Text speedUpPrice;
	public Text moneyEarningPrice;

	List<float> tenLvlMut;
	List<float> tenOfflineMut;

	static Skill instance;
	public static Skill Instance{
		get{ return instance;}
	}
	void Awake(){
		instance = this;
	}

	void Start(){
		//添加前十等级的价格递增倍数
		tenLvlMut = new List<float>();
		InitTenLvl ();

		//添加前十等级的离线奖励倍数
		tenOfflineMut = new List<float>();
		InitTenOffline ();

		//更新ui显示
		UpdateSkillState ();
		OnMaxBtn ();

	}
		
	//切换升级类别
	public void OnMaxBtn(){
		max.SetActive (true);
		maxBtn.interactable = false;
		speedUp.SetActive (false);
		speedUpBtn.interactable = true;
		earning.SetActive (false);
		earningBtn.interactable = true;
	}

	//切换升级类别
	public void OnSpeedUpBtn(){
		max.SetActive (false);
		maxBtn.interactable = true;
		speedUp.SetActive (true);
		speedUpBtn.interactable = false;
		earning.SetActive (false);
		earningBtn.interactable = true;
	}

	//切换升级类别
	public void OnEarningBtn(){
		max.SetActive (false);
		maxBtn.interactable = true;
		speedUp.SetActive (false);
		speedUpBtn.interactable = true;
		earning.SetActive (true);
		earningBtn.interactable = false;
	}

	//购买最大速度
	public void OnMaxBuyBtn(){		
		float foreSpeed = PlayerPrefs.GetFloat ("maxSpeedValue", 5);
		float afterSpeed = foreSpeed + 0.25f;
		int price = PlayerPrefs.GetInt ("maxSpeedPrice", 1000);
		int lvl = PlayerPrefs.GetInt ("maxSpeedLvl", 1);
		PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt ("gold", 0) - price);
		PlayerPrefs.SetInt ("maxSpeedLvl", lvl+1);
		PlayerPrefs.SetFloat ("maxSpeedValue", afterSpeed);
		PlayerPrefs.SetInt ("maxSpeedPercentage", (int)(((afterSpeed - foreSpeed) / foreSpeed * 100)));
		PlayerPrefs.SetInt ("maxSpeedPrice", (int)(price * GetLvlMut (lvl)));
		UpdateSkillState ();
	}

	//购买最大加速度
	public void OnSpeedBuyBtn(){
		float foreSpeed = PlayerPrefs.GetFloat ("speedUpValue", 15);
		float afterSpeed = foreSpeed + 0.5f;
		int price = PlayerPrefs.GetInt ("speedUpPrice", 1520);
		int lvl = PlayerPrefs.GetInt ("speedUpLvl", 1);
		PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt ("gold", 0) - price);
		PlayerPrefs.SetInt ("speedUpLvl", lvl+1);
		PlayerPrefs.SetFloat ("speedUpValue", afterSpeed);
		PlayerPrefs.SetInt ("speedUpPercentage", (int)(((afterSpeed - foreSpeed) / foreSpeed * 100)));
		PlayerPrefs.SetInt ("speedUpPrice", (int)(price * GetLvlMut (lvl)));
		UpdateSkillState ();
	}

	//购买离线奖励
	public void OnEarningBuyBtn(){
		int fore = PlayerPrefs.GetInt ("moneyEarningValue", 10);
		int lvl = PlayerPrefs.GetInt ("moneyEarningLvl", 1);
		int after = (int)(fore * GetOfflineMut(lvl));
		int price = PlayerPrefs.GetInt ("moneyEarningPrice", 2500);
		PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt ("gold", 0) - price);
		PlayerPrefs.SetInt ("moneyEarningLvl", lvl+1);
		PlayerPrefs.SetInt ("moneyEarningValue", after);
		PlayerPrefs.SetInt ("moneyEarningPercentage", (int)(((after - fore) / fore * 100)));
		PlayerPrefs.SetInt ("moneyEarningPrice", (int)(price * GetLvlMut (lvl)));
		UpdateSkillState ();
	}


	public void UpdateSkillState(){
		UpdateText ();
		UpdateButton ();
	}

	//更新文案
	void UpdateText(){
		gold.text = "$" + Conversion.UnitChange (PlayerPrefs.GetInt ("gold", 0));

		//等级
		maxSpeedLvl.text = "LVL." + PlayerPrefs.GetInt ("maxSpeedLvl", 1).ToString ();
		speedUpLvl.text = "LVL." + PlayerPrefs.GetInt ("speedUpLvl", 1).ToString ();
		moneyEarningLvl.text = "LVL." + PlayerPrefs.GetInt ("moneyEarningLvl", 1).ToString ();

		//Value
		maxSpeedValue.text = (PlayerPrefs.GetFloat ("maxSpeedValue", 5)*10).ToString () + " MPH";
		speedUpValue.text = PlayerPrefs.GetFloat ("speedUpValue", 15).ToString () + " G";
		moneyEarningValue.text = PlayerPrefs.GetInt ("moneyEarningValue", 10).ToString () + " X";

		//百分比
		maxSpeedPercentage.text = "+" + PlayerPrefs.GetInt ("maxSpeedPercentage", 0).ToString () + "%";
		speedUpPercentage.text = "+" + PlayerPrefs.GetInt ("speedUpPercentage", 0).ToString () + "%";
		moneyEarningPercentage.text = "+" + PlayerPrefs.GetInt ("moneyEarningPercentage", 0).ToString () + "%";

		//升级金钱
		maxSpeedPrice.text = "$" + Conversion.UnitChange(PlayerPrefs.GetInt ("maxSpeedPrice", 1000));
		speedUpPrice.text = "$" + Conversion.UnitChange(PlayerPrefs.GetInt ("speedUpPrice", 1520));
		moneyEarningPrice.text = "$" + Conversion.UnitChange(PlayerPrefs.GetInt ("moneyEarningPrice", 2500));
	}

	//更新按钮
	void UpdateButton(){
		int goldInt = PlayerPrefs.GetInt ("gold", 0);
		int speedUpPriceInt = PlayerPrefs.GetInt ("speedUpPrice", 1520);
		int moneyEarningPriceInt = PlayerPrefs.GetInt ("moneyEarningPrice", 2500);
		int maxSpeedPriceInt = PlayerPrefs.GetInt ("maxSpeedPrice", 1000);

		speedUpBuyBtn.interactable = goldInt > speedUpPriceInt;
		maxBuyBtn.interactable = goldInt > maxSpeedPriceInt;
		earningBuyBtn.interactable = goldInt > moneyEarningPriceInt;
	}

	void InitTenLvl(){
		tenLvlMut.Add (8.52f);
		tenLvlMut.Add (3.847f);
		tenLvlMut.Add (3.05f);
		tenLvlMut.Add (2.416f);
		tenLvlMut.Add (1.822f);
		tenLvlMut.Add (1.475f);
		tenLvlMut.Add (1.265f);
		tenLvlMut.Add (1.109f);
		tenLvlMut.Add (1.049f);
		tenLvlMut.Add (1.048f);
	}

	void InitTenOffline(){
		tenOfflineMut.Add (22.4f);
		tenOfflineMut.Add (2.026f);
		tenOfflineMut.Add (1.5f);
		tenOfflineMut.Add (1.36f);
		tenOfflineMut.Add (1.27f);
		tenOfflineMut.Add (1.21f);
		tenOfflineMut.Add (1.17f);
		tenOfflineMut.Add (1.14f);
		tenOfflineMut.Add (1.11f);
		tenOfflineMut.Add (1.1f);
	}

	//获取升级倍数
	float GetLvlMut(int lvl){
		if (lvl <= 10) {
			return tenLvlMut [lvl];
		} else if (lvl <= 50) {
			return tenLvlMut [10] - (lvl - 10) * 0.0005f;
		} else {
			return GetLvlMut (50);
		}
	}

	//获取离线奖励倍数
	float GetOfflineMut(int lvl){
		if (lvl <= 10) {
			return tenOfflineMut [lvl];
		} else if (lvl <= 30) {
			return tenOfflineMut [10] - (lvl - 10) * 0.005f;
		} else {
			return GetOfflineMut (30);
		}
	}
}
