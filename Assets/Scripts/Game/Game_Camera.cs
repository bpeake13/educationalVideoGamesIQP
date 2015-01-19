using UnityEngine;
using System.Collections;

public class Game_Camera : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float aspect = Camera.main.aspect;
		float offset = ((1/aspect) - (1/(16f/9f))) * 24;
		Debug.Log (offset);
		Camera.main.transform.position = new Vector3(-offset,
		                                             Camera.main.transform.position.y,
		                                             Camera.main.transform.position.z);
		transform.position = new Vector3(-offset,
		                                 transform.position.y,
		                                 transform.position.z);
	}
}
