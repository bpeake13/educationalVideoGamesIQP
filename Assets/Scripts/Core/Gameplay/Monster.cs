using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	private Counter health = new Counter(); // The health of the mob
	private bool isActive = true; // Whether the mob will be targeted in attacks
	private bool isDestroyed = false; // Whether the mob's health has been reduced to 0
	private GameObject md; // The music driver object
	private TestMusicDriver mdscript;

	// Use this for initialization
	void Start () {
		health.setMaxValue(Random.Range (4, 9));
		health.setValue(100);
		Debug.Log ("enemy health set to " + health.getValue());
		md = GameObject.Find("MusicDriver");
		mdscript = (TestMusicDriver) md.GetComponent(typeof(TestMusicDriver));
		//mdscript.Play();
	}
	
	// Update is called once per frame
	void Update () {
		// Update things
		// Check if on beat
		if(Input.GetKeyDown ("space")) {
			if(mdscript.GetBeatEdge()) {
				onHit (1);
			} else {
				// Whoops, missed the beat
				Debug.Log ("missed the beat");
			}
		}
	}

	// Things that occur when the mob is hit
	void onHit(int damage) {
		if(isActive) {
			health.subtract (damage);
			// Do other things related to animation, scoring, etc. here
			Debug.Log ("hit registered");
			if(health.getValue() <= 0 &&!isDestroyed) {
				Debug.Log ("enemy destroyed");
				isDestroyed = true;
			}
		}
	}
}
