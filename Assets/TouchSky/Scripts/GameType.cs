using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameType : MonoBehaviour {
	int i = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeGameType(){
		if (i == 9) {
			i = 0;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_TV_Video3D> ());
		}
		else if (i == 8) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Drawing_Toon> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_TV_Video3D> ();
		}
		else if (i == 7) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Drawing_Paper3> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_Drawing_Toon> ();
		}
		else if (i == 6) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Drawing_Paper2> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_Drawing_Paper3> ();
		}
		else if (i == 5) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Drawing_Paper> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_Drawing_Paper2> ();
		}
		else if (i == 4) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Drawing_Manga5> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_Drawing_Paper> ();
		}
		else if (i == 3) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Drawing_Manga4> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_Drawing_Manga5> ();
		}
		else if (i == 2) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Drawing_Manga2> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_Drawing_Manga4> ();

		}
		else if (i == 1) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Drawing_BluePrint> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_Drawing_Manga2> ();
		}
		else if (i == 0) {
			i++;
			Camera.main.gameObject.AddComponent<CameraFilterPack_Drawing_BluePrint> ();

		}
	}
}
