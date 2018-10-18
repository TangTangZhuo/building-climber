using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
 
	public static void ToDrawCircle(Transform trans,Vector3 center,float radius){
		LineRenderer lineRenderer = trans.GetComponent<LineRenderer> ();
		if (lineRenderer == null) {
			lineRenderer = trans.gameObject.AddComponent<LineRenderer> ();
		}
		lineRenderer.startWidth = 0.05f;
		lineRenderer.endWidth = 0.05f;
		lineRenderer.useWorldSpace = false;

		int pointCount = 100;
		float eachAngle = 360f / pointCount;
		int space = pointCount / 5;
		int spaceConter = 0;
		//Vector3 forward = trans.up;
		lineRenderer.positionCount = pointCount + 1;

		for (int i = 0; i <= pointCount; i++) {
			

			if (spaceConter == 0) {
				Vector3 pos = Quaternion.Euler (0f, 0f, eachAngle * i) * Vector3.up * radius + center;
				lineRenderer.SetPosition (i, pos);
			}
		}
	}
}
