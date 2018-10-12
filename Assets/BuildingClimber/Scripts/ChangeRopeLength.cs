using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityChan;
using Anima2D;

public class ChangeRopeLength : MonoBehaviour {

	//public delegate void ResetRopeFinish();
	//public event ResetRopeFinish ResetRopeCallBack;

	public Transform[] bones;
	public Transform player;

	public bool resetRope = true;

	Bone2D bone2d;

	Animator animator;
	PlayerController playerController;

	public static ChangeRopeLength Instance{
		get{return instance;}
	}
	private static ChangeRopeLength instance;
	void Awake(){
		instance = this;
	}
	// Use this for initialization
	void Start () {
		playerController = player.GetComponent<PlayerController> ();
		animator = playerController.animator;
		bone2d = bones [0].GetComponent<Bone2D> ();
		playerController.jumpFinish += () => {
			resetRope = true;	
		};
	}
	
	// Update is called once per frame
	void Update () {
		
		//初始化绳子长度
		ResetRope ();

		//完成初始化可进行投掷
		if (!resetRope) {
			//按下space进入准备投掷和绳子伸缩阶段
			if (Input.GetKeyDown (KeyCode.Space)) {
				animator.SetBool ("Ready", true);
			} else if (Input.GetKeyUp (KeyCode.Space)) {
				animator.SetBool ("Ready", false);
				SetStiffness (10);
			}			
			AnimatorStateInfo animatorInfo = animator.GetCurrentAnimatorStateInfo (1);
			if (animatorInfo.IsName ("GoHook")) {
				float duTime = animatorInfo.normalizedTime - (int)animatorInfo.normalizedTime;
				Vector3 boneV3 = bones [1].position - bones [0].position;
				float boneLength = boneV3.magnitude;
				//手下摆时伸长，上摆时缩短
				if (duTime <= 0.5f) {				
					if (boneLength < 1.5f) {
						ChangeLength (false, 0.95f);
					}
				} else {				
					if (boneLength > 0.1f) {
						ChangeLength (true, 0.95f);
					}
				}
			}
		}
	}

	void ResetRope(){
		if (resetRope) {
			Vector3 boneV3 = bones [1].position - bones [0].position;
			float boneLength = boneV3.magnitude;
			if (boneLength < 0.1f) {
				resetRope = false;
				return;
			}
			ChangeLength (true, 10);
		}
	}

	public void SetStiffness(float stiffness){
		for (int i = 0; i < bones.Length; i++) {
			bones [i].GetComponent<SpringBone> ().stiffnessForce = stiffness;
		}
	}

	public void ChangeLength(bool state,float speed){
		
		for (int i = bones.Length - 1; i >= 1; i--) {
			Vector3 dir = Vector3.one;
			if (state == true) {
				dir = bones [i - 1].position - bones [i].position;
			} else {
				dir = bones [i].position - bones [i - 1].position;
			}



			bones [i].position = Vector3.Lerp (bones [i].position, bones [i].position + dir.normalized/10, speed * 10 * Time.deltaTime);
		}


	}
}
