using UnityEngine;
using System.Collections;

public class Profile_UI : MonoBehaviour {

	public string stringToEdit = "Enter your username here";
	Student_Data sd;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	private GameObject s_io; // IO
	private Student_IO ioscript;

	public GUIStyle bigFont;

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
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown ("enter")) {
			Proceed ();
		}
	}

	// Inits student profile and starts the next level
	void Proceed() {
		string filepath = Application.dataPath.Remove(Application.dataPath.Length - 7) + @"/Student Data/";
		sd = ioscript.Import(filepath + stringToEdit + ".txt");
		sd.s_name = stringToEdit;
		spscript.setStudentData(sd);
		// Change scenes to gameplay
		Application.LoadLevel ("Menu2");
	}

	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
		GUI.Box(new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), "");
		if (GUI.Button (new Rect (Screen.width/2 -  50, Screen.height/2 + 20, 100, 20), "Continue")) {
			Proceed();
		}
		GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/4 + 20,100,50), "Login", bigFont);
		stringToEdit = GUI.TextField(new Rect(Screen.width/2 - 110, Screen.height/2 - 10, 220, 20), stringToEdit, 25);
	}
}
