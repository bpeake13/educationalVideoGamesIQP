using UnityEngine;
using System.Collections;

public class Track_Behavior : MonoBehaviour {

	private float lifetimer = 0f;
	public int onScreenBars = 8;
	public int bpm = 120;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate (new Vector3(-3f * Time.deltaTime, 0f, 0f));
		transform.position = new Vector3(((((60f/(float)bpm) - lifetimer/onScreenBars)/(60f/(float)bpm))*35f - 15f), transform.position.y, transform.position.z);
		lifetimer += Time.deltaTime;
		if(transform.position.x < -22f) {
			Destroy (gameObject);
			lifetimer = 0f;
		}
	}
}
