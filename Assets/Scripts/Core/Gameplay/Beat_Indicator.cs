using UnityEngine;
using System.Collections;

public class Beat_Indicator : MonoBehaviour {

	private GameObject md; // The music driver object
	private TestMusicDriver mdscript;

	// Use this for initialization
	void Start () {
		md = GameObject.Find("MusicDriver");
		mdscript = (TestMusicDriver) md.GetComponent(typeof(TestMusicDriver));
	}
	
	// Update is called once per frame
	void Update () {
		if(mdscript.isOnBeat()) {
			renderer.enabled = true;
		} else {
			// Commented out
			//renderer.enabled = false;
		}
	}

	// GUI methods related to monsters
	void OnGUI () {
		// Put this somewhere else sooner or later
		//if(mdscript.isOnBeat()) {
		//	GUI.Label (new Rect (10,35,1000,50), "ATTACK NOW");
		//}
	}
}
