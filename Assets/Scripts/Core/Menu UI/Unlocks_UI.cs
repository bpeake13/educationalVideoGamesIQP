using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unlocks_UI : MonoBehaviour {

	public Texture texture1;
	private bool passwordFailed = false;
	private List<string> achievements = new List<string>();
	Student_Data sd;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	private GameObject s_io; // IO
	private Student_IO ioscript;
	
	public GUIStyle failedFont;
	public GUIStyle bigFont;
	public GUIStyle stdFont;
	public GUIStyle achievementFont;
	
	//string filepath = @"C:\Users\Benjamin\Documents\IQP Project\Student Data\";
	
	// Use this for initialization
	void Start () {
		// Student Profile script
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		sd = spscript.getStudentData();
		// IO Script
		s_io = GameObject.Find("Student_IO");
		ioscript = (Student_IO) s_io.GetComponent(typeof(Student_IO));

		// Add some arbitrary acheivements
		achievements.Add ("Complete one game");
		achievements.Add ("Complete four games");
		achievements.Add ("Complete ten games");
		achievements.Add ("Complete twenty games");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown ("enter")) {
			Proceed ();
		}
	}
	
	// Inits student profile and starts the next level
	void Proceed() {
		// Change scenes to menu2
		Application.LoadLevel ("Menu2");
		string filepath = Application.dataPath + @"/Student Data/";
		ioscript.Export(filepath, sd);
	}
	
	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
		GUI.Box(new Rect(Screen.width/8, Screen.height/8, Screen.width*3/4, Screen.height/8 + 20), "");
		GUI.Box(new Rect(Screen.width/2, Screen.height/4 + 30, Screen.width*1/3 - 10, Screen.height/4), "");
		for(int i = 0; i < achievements.Count; i++) {
			GUI.Box(new Rect(Screen.width/8, Screen.height/4 + i * 80 + 30, Screen.width*3/8 - 10, Screen.height/8), "");
			GUI.Label (new Rect(Screen.width/8 + 60, Screen.height/4 + i * 80 + 30, Screen.width*1/4, Screen.height/8), achievements[i], achievementFont);
			GUI.Box(new Rect(Screen.width/8 + 10, Screen.height/4 + i * 80 + 30 + 20, 40, 40), texture1);
		}
		//if (GUI.Button (new Rect (Screen.width/2 -  50, Screen.height/2 + 50, 100, 20), "Continue")) {
		//	Proceed();
		//}
		GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/8 + 20,100,50), "Unlock Things!", bigFont);
		GUI.Label (new Rect (Screen.width/2 - 100, Screen.height/8 + 65,200,50), "Acheivements Unlocked: 0/4", stdFont);
		if (GUI.Button (new Rect (Screen.width - 110, Screen.height * (9f/10f), 100, 40), "Go Back")) {
			// Change to stats screen
			Application.LoadLevel ("Menu2");
		}
		if (GUI.Button (new Rect (Screen.width*3/5, Screen.height/3, 200, 40), "Get a new random avatar!")) {

		}
	}
}
