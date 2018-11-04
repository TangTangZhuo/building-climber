using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	}

	public void OnCollectBtn(){
		PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt("gold",0) + PlayerPrefs.GetInt ("CurGold", 0) );
		PlayerControllerSky.GameEnd ();
	}

	public void OnDoubleBtn(){
		PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt("gold",0) + PlayerPrefs.GetInt ("CurGold", 0)*2 );
		PlayerControllerSky.GameEnd ();
	}
}
