using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBGColor : MonoBehaviour {



	// Use this for initialization
	void Start () {
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		sprite.color = new Color(Random.Range(0,256)/255f,Random.Range(0,256)/255f,Random.Range(0,256)/255f);
	}

}
