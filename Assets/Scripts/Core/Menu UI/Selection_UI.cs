using UnityEngine;
using System.Collections;

public class Selection_UI : MonoBehaviour {

	public GUIStyle bigFont;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
		if (GUI.Button (new Rect (Screen.width/8 - 10, Screen.height/4, Screen.width*(3f/8f), Screen.height/2), "Play Game")) {
			// Change scenes to gameplay
			Application.LoadLevel ("Game");
		}
		if (GUI.Button (new Rect (Screen.width/2 + 10, Screen.height/4, Screen.width*(3f/8f), Screen.height/2), "View Stats")) {
			// Change to stats screen
			Application.LoadLevel ("Stats");
		}
	}
}
