﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldCollect : MonoBehaviour {
	Transform goldImage;

	void Start () {
		Invoke ("GetGoldImage", 0.2f);
	}

	void GetGoldImage(){
		if (GameObject.FindGameObjectWithTag ("goldImage")) {
			goldImage = GameObject.FindGameObjectWithTag ("goldImage").transform;
		}else{
			Invoke ("DestroyImage", 1f);
		}
	}

	void DestroyImage(){
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "goldCollider") {
			if (TurnTable.Instance) {
				TurnTable.Instance.AddMultiText ();
			
				goldImage.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.2f).OnComplete (() => {
					goldImage.DOScale (1f, 0.2f);
					Destroy (gameObject);
				});
			}
		}

		if (col.tag == "Treasure") {
			goldImage.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.2f).OnComplete(()=>{
				goldImage.DOScale (1f, 0.2f);
				Destroy(gameObject);
			});
		}


	}
}
