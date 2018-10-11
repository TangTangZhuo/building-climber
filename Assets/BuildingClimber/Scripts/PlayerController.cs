using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
	[HideInInspector]
	public Animator animator;
	// Use this for initialization

	public delegate void JumpFinishCallBack();
	public event JumpFinishCallBack jumpFinish;

	public static PlayerController Instance{
		get{ return instance;}
	}
	private static PlayerController instance;
	void Awake(){
		instance = this;
		animator = transform.GetChild(0).GetComponent<Animator> ();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {		

		//上墙动作完成后播放上墙待机动画
		AnimatorStateInfo animatorInfo0 = animator.GetCurrentAnimatorStateInfo(0);
		if (animatorInfo0.IsName ("GoStart") && animatorInfo0.normalizedTime >= 1) {
			animator.SetBool ("GoStartFinish", true);
		}

	}

	//跳跃，x为高度，y为距离，x和y不能为负
	public void PlayerJump(float x,float y){
		if (x < 0 || y < 0)
			return;
		transform.DOMove(transform.position+new Vector3(-x,y,0),0.5f,false).OnComplete(()=>{
			Vector3[] path = GetCirclePath(transform.position,transform.position+new Vector3(x,y,0),10);
			transform.DOPath(path,0.5f,PathType.CatmullRom,PathMode.TopDown2D,10,null).OnComplete(()=>{
				jumpFinish();
			});
		}).SetEase(Ease.InBack);
	}

	//已知高度距离的抛物线方程
	float GetCircleY(float high,float x,float distance){
		return Mathf.Sqrt (high * high - (x - high) * (x - high)) / high * distance;
	}

	//已知起始位置和结束位置返回两点间抛物线的点坐标
	Vector3[] GetCirclePath(Vector3 startPos,Vector3 endPos,int pointNum){
		float high = endPos.x - startPos.x;
		float distance = endPos.y - startPos.y;
		float space = high / pointNum;
		List<Vector3> points = new List<Vector3> ();
		for (int i = 1; i <= pointNum; i++) {
			points.Add (transform.position + new Vector3 (space * i, GetCircleY (high,space * i,distance), 0));
		}
		Vector3[] pointsArray = new Vector3[points.Count];
		for (int i = 0; i < pointNum; i++) {
			pointsArray [i] = points [i];
		}
		return pointsArray;
	}
}
