using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Together;

public class RevivePop : MonoBehaviour {
	public Button confirmBtn;
	public Text text;

	public GameObject startRocket;

	ThrowHook throwHook;

	float time = 6;

	void OnEnable(){
		confirmBtn.interactable = false;
		if (TGSDK.CouldShowAd (TZ_TGSDK.reviveID)) {
			confirmBtn.interactable = true;
		}

		throwHook = GameObject.FindGameObjectWithTag ("Player").GetComponent<ThrowHook> ();
		throwHook.isStart = false;

		StartCoroutine (CountDown ());
	}

	public void OnBackBtn(){
		gameObject.SetActive (false);
		Invoke ("GameEnd", 0.2f);
	}

	public void OnConfirmBtn(){
		TGSDK.ShowAd (TZ_TGSDK.reviveID);
		TGSDK.AdCompleteCallback = (string obj) => {
			Revive();
		};
		TGSDK.AdCloseCallback = (string obj) => {
			OnBackBtn();
		};
		TGSDK.AdShowFailedCallback = (string obj) => {
			OnBackBtn();
		};
		TGSDK.AdRewardFailedCallback = (string obj) => {
			OnBackBtn();
		};
	}

	void Revive(){
		gameObject.SetActive (false);
		throwHook.gameState = GameState.isInSky;
		if (throwHook.curHook) {
			Destroy (throwHook.curHook);
		}
		throwHook.GenerateCircle ();
		throwHook.isStart = true;
		Instantiate (startRocket, throwHook.transform.position+Vector3.up, Quaternion.identity);
	}

	void GameEnd(){
		PlayerControllerSky.GameEnd ();
	}

	IEnumerator CountDown(){		
		while (time >= 1) {
			time -= Time.deltaTime / Time.timeScale;
			text.text = ((int)time).ToString ();
			yield return null;
		}
		time = 6;
		OnBackBtn ();
	}
}
