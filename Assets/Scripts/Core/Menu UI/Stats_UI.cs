using UnityEngine;
using System.Collections;

public class Stats_UI : MonoBehaviour {

	Student_Data sd;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;

	public GUIStyle bigFont;
	public GUIStyle mediumFont;

	// Use this for initialization
	void Start () {
		// Student Profile script
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		sd = spscript.getStudentData();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
		GUI.Box(new Rect(Screen.width/8, Screen.height/8, Screen.width * (3f/4f), Screen.height * (3f/4f)), "");
		GUI.Label (new Rect (Screen.width/2 - 150, Screen.height/6,300,50), "Hi, " + sd.s_name, bigFont);
		GUI.Label (new Rect (Screen.width/2 - 150, Screen.height/6 + 50,300,50), "This is your progress so far...", bigFont);
		if (GUI.Button (new Rect (Screen.width * (9f/10f), Screen.height * (9f/10f), 100, 40), "Go Back")) {
			// Change to stats screen
			Application.LoadLevel ("Menu2");
		}
		// Display Stats
		GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + 66f + 20f,300 + 20f,50), "Best Score: " + sd.bestScore, mediumFont);
		GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + 133f + 20f,300 + 20f,50), "Total Score: " + sd.totalScore, mediumFont);
		GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + 200f + 20f,300 + 20f,50), "Mean Score: " + sd.meanScore, mediumFont);
		GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + 266f + 20f,300 + 20f,50), "Attempts: " + sd.attempts, mediumFont);
	}
}
