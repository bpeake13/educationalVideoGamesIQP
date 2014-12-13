using UnityEngine;
using System.Collections;

public class Track_Spawner : MonoBehaviour {

	public GameObject track;
	private static float timer = 0f;

	// Use this for initialization
	void Start () {
		//for(int i = 0; i < 10; i++) {
			//GameObject tp = (GameObject)Instantiate (track, new Vector3(i * 2.0F - 7.47f, 0.836f, 4.89f), Quaternion.Euler(270, 0, 0));
		//}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > 0.5f) {
			GameObject o = (GameObject)Instantiate (track, new Vector3( 20f, 0.836f, 4.89f), Quaternion.Euler(270, 0, 0));
			//o = 
			timer -= 0.5f;
		}
	}
}
