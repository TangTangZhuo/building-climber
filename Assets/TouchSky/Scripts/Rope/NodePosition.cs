using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePosition : MonoBehaviour {

	//绳子核心算法：
	//判断两个节点的距离，如果超过节点定长，则当前距离变为定常距离（改变子节点位置，用插值来计算）
	private float nodeDistance = 1.6f;  //节点定长
	public Transform lastNodeTran;   //上一个节点的位置
	float realDistance;      //真实距离
	Rigidbody rig;
	float allowValue = 0.02f;  //允许误差的值

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
	}
	private void Update()
	{
		Judge();
	//	Deceleration();
	}
	void Judge()
	{
		//求两点距离时，用平方会比开方好
		realDistance = Vector3.Magnitude(lastNodeTran.position-transform.position);
		if (realDistance>nodeDistance) //用平方来比较
		{
			//已经超过
			transform.position = Vector3.Lerp(lastNodeTran.position, transform.position, nodeDistance / realDistance);
		//	transform.position = Vector3.Lerp(transform.position,lastNodeTran.position,Time.deltaTime);
		}
	}
	void Deceleration() //减速
	{
		if (rig.velocity.sqrMagnitude>25)
		{
			rig.velocity /= 3;
		}
	}
}
