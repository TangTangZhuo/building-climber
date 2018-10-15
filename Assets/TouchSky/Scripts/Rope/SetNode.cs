using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNode : MonoBehaviour {

	//控制绳子的显示
	//获得LineRender组件，设置Size等于节点数，设置节点的位置
	public Transform node0;
	public Transform node1;
	public Transform node2;
	public Transform node3;
	public Transform node4;
	public Transform node5;

	Vector3[] allNodes;

	LineRenderer lineRen;

	private void Start()
	{
		lineRen = GetComponent<LineRenderer>();
		Debug.Log(lineRen);
		//lineRen.positionCount = 6;
		//新建一个数组
		lineRen.positionCount = 6;


		Debug.Log(lineRen.positionCount);
	}
	private void Update()
	{
		allNodes = new Vector3[6] { node0.position, node1.position, node2.position, node3.position, node4.position, node5.position };
		lineRen.SetPositions(allNodes);
	}
}
