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
		if (GUI.Button (new Rect (Screen.width * (9f/10f), Screen.height * (9f/10f), 100, 40), "Quit")) {
			// Quit the application
			Application.Quit();
		}
		if (GUI.Button (new Rect (Screen.width/8 - 10, Screen.height/4, Screen.width*(2f/8f), Screen.height/2), "Play Game")) {
			// Change scenes to gameplay
			Application.LoadLevel ("Game");
		}
		if (GUI.Button (new Rect (Screen.width*3/8, Screen.height/4, Screen.width*(2f/8f), Screen.height/2), "View Stats")) {
			// Change to stats screen
			Application.LoadLevel ("Stats");
		}
		if (GUI.Button (new Rect (Screen.width*5/8 + 10, Screen.height/4, Screen.width*(2f/8f), Screen.height/2), "Unlock Things!")) {
			// Change to stats screen
			Application.LoadLevel ("unlocks");
		}
	}
}
