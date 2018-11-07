using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyGold : MonoBehaviour {
	public Transform flyGold;
	public Transform generatePos;
	public Transform targetPos;


	public void FlyGoldGenerate(){
		FlyGoldGenerate (targetPos);
	}

	public void FlyGoldGenerate(Transform tarPos){
		for (int numr = 0; numr < 10; numr++) {
			Vector3 goldPostion = generatePos.position+new Vector3(Random.Range(1f,4f),Random.Range(-2f,2f),0);
			GenerateFlyGoldWithPos (goldPostion,Random.Range(-0.6f,-0.4f),tarPos,false);
		}

		for (int numl = 0; numl < 10; numl++) {
			Vector3 goldPostion = generatePos.position+new Vector3(Random.Range(-2f,-1f),Random.Range(-2f,2f),0);
			GenerateFlyGoldWithPos (goldPostion,Random.Range(0.4f,0.6f),tarPos,false);
		}
	}

	public void FlyGoldGenerate(int number){
		for (int numr = 0; numr < number/2; numr++) {
			Vector3 goldPostion = generatePos.position+new Vector3(Random.Range(1f,4f),Random.Range(-2f,2f),0);
			GenerateFlyGoldWithPos (goldPostion,Random.Range(-0.6f,-0.4f),targetPos,true);
		}

		for (int numl = 0; numl < (number-number/2); numl++) {
			Vector3 goldPostion = generatePos.position+new Vector3(Random.Range(-2f,-1f),Random.Range(-2f,2f),0);
			GenerateFlyGoldWithPos (goldPostion,Random.Range(0.4f,0.6f),targetPos,true);
		}
	}


	void GenerateFlyGoldWithPos(Vector3 goldPo,float offsetV3X,Transform tarPos,bool isChange){
		Vector3 goldPostion = goldPo;

		//float offsetV3X = -0.5f;
		float offsetV3Y = Random.Range(-3.5f,-2.5f);
		float offsetTime = Random.Range(0.1f,1);
		Vector3 goldRotation = flyGold.rotation.eulerAngles+new Vector3(0,0,Random.Range(0,360));
		Transform flygold;
		if (isChange) {
			flygold = Transform.Instantiate (flyGold, goldPostion, Quaternion.Euler (goldRotation),GameObject.Find ("Canvas").transform);
		} else {
			flygold = Transform.Instantiate (flyGold, goldPostion, Quaternion.Euler (goldRotation));
		}
		Vector3 targetV3 = tarPos.position - goldPostion;
		Vector3 offsetV3 = new Vector3 (offsetV3X, offsetV3Y, 0);
		float time = Random.Range (1.5f, 2.5f);
		flygold.DOBlendableMoveBy (targetV3-offsetV3,time).OnComplete (() => {
			
		}).SetEase(Ease.InOutQuart);
		flygold.DOBlendableMoveBy (offsetV3, offsetTime);

		ChangeSize (time, flygold, isChange);
	}

		
	void ChangeSize(float time,Transform trans,bool isChange){
		if (isChange) {
			trans.DOScale (1, time / 2).OnComplete (() => {
				trans.DOScale (0.5f, time / 2);
			});
		}
	}

}
