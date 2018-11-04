using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {	
	public Text curLevel;
	public Text nextLevel;

	// Use this for initialization
	void Start () {
		UpdateTextState ();
	}

	public void UpdateTextState(){
		int cur = PlayerPrefs.GetInt ("curLevel", 1);
		curLevel.text = cur.ToString();
		nextLevel.text = (cur + 1).ToString ();
	}
}
