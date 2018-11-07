using UnityEngine;

namespace SplatterSystem {

	public class SpawnByPos : MonoBehaviour {
		public AbstractSplatterManager splatter;
		public Transform trans;

		public void SpawnDoodle(){
			Vector2 worldPos = trans.position;
			splatter.Spawn (worldPos);
		}
	}

}
