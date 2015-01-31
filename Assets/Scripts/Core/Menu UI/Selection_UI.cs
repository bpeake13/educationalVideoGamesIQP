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
		GUI.Box(new Rect(Screen.width/(3.5f), Screen.height/6 - 10, (Screen.width - Screen.width/(3.5f)*2), Screen.height*(2/3f) + 10), "");
		GUI.Label(new Rect(Screen.width/2, Screen.height/6 + 45, 0, 0), "Main Menu", bigFont);
		if (GUI.Button (new Rect (Screen.width - 110, Screen.height * (9f/10f), 100, 40), "Quit")) {
			// Quit the application
			Application.Quit();
		}
		if (GUI.Button (new Rect (Screen.width/2 - 90, Screen.height*(1.5f/6f) + Screen.height/24 + 30, 180, Screen.height/16), "Play Game")) {
			// Set a player pref so the game knows which screen to return to
			PlayerPrefs.SetString( "GameReturnScreen", "Menu2" );
			// Change scenes to gameplay
			Application.LoadLevel ("Game");
		}
		if (GUI.Button (new Rect (Screen.width/2 - 90, Screen.height*(2.16f/6f) + Screen.height/24 + 30, 180, Screen.height/16), "View Stats")) {
			// Change to stats screen
			Application.LoadLevel ("Stats");
		}
		if (GUI.Button (new Rect (Screen.width/2 - 90, Screen.height*(2.83f/6f) + Screen.height/24 + 30, 180, Screen.height/16), "Unlock Things!")) {
			// Change to stats screen
			Application.LoadLevel ("unlocks");
		}
		if (GUI.Button (new Rect (Screen.width/2 - 90, Screen.height*(3.5f/6f) + Screen.height/24 + 30, 180, Screen.height/16), "Go to Editor Menu")) {
			// Change to stats screen
			Application.LoadLevel ("EditorMainMenu");
		}
	}
}
