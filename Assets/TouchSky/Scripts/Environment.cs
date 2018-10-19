using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

	int i=0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void ChangeEnvironment(){
		if (i == 5) {
			i = 0;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Blizzard> ());
		}
		else if (i == 4) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_3D_Snow> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_Blizzard> ();
		}
		else if (i == 3) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Atmosphere_Rain> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_3D_Snow> ();
		}
		else if (i == 2) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Atmosphere_Rain_Pro> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain> ();

		}
		else if (i == 1) {
			i++;
			Destroy (Camera.main.gameObject.GetComponent<CameraFilterPack_Atmosphere_Rain_Pro_3D> ());
			Camera.main.gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain_Pro> ();
		}
		else if (i == 0) {
			i++;
			Camera.main.gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain_Pro_3D> ();
		}
	}
}
