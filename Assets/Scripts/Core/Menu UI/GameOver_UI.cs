using UnityEngine;
using System.Collections;

public class GameOver_UI : MonoBehaviour {

	public GUIStyle FinalFont;
	public GUIStyle smallFont;

	// The currently loaded student data
	Student_Data studentData;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	
	private GameObject io;
	private Student_IO IOScript = new Student_IO();
	
	private GameObject gm; // The game metric object
	private Game_Metric gmscript;
	
	//string filepath = @"C:\Users\Benjamin\Documents\IQP Project\Student Data\";
	//string filepath = Application.dataPath + @"/Student Data/";
	
	// Use this for initialization
	void Start () {
		// Application.dataPath.Remove(Application.dataPath.Length - 7) + @"/Student Data/";
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		studentData = spscript.getStudentData();
		io = GameObject.Find("Student_IO");
		IOScript = (Student_IO) io.GetComponent(typeof(Student_IO));
		gm = GameObject.Find("GameMetric");
		gmscript = (Game_Metric) gm.GetComponent(typeof(Game_Metric));
	}

	// Update is called once per frame
	void Update () {
		string filepath = Application.dataPath + @"/Student Data/";
		if(Input.GetKeyDown (KeyCode.Escape)) {
			string grs = PlayerPrefs.GetString( "GameReturnScreen" );
			// Update the student metrics if one did not come from the editor
			if(grs == "Menu2") {
				// Update student data
				//UpdateStudentMetrics ();
				// Export the data
				//IOScript.Export(filepath, studentData);
			}
			// Goto title screen or editor based on previous screen
			Application.LoadLevel( grs );
		} else if(Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel( "Game" );
		}
	}

	void OnGUI() {
		GUI.Box(new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), "");
		GUI.Label (new Rect(Screen.width/4, Screen.height/2 - 40, Screen.width/2, Screen.height/2), 
		           "GAME OVER", FinalFont);
		if((int)(Time.time*2) % 2 == 0) {
			GUI.Label (new Rect(Screen.width/4, Screen.height/2 + 20, Screen.width/2, Screen.height/2), 
			           "Press ESC to return", FinalFont);
		}
		GUI.Label (new Rect(Screen.width/4, Screen.height/2 + 80, Screen.width/2, Screen.height/2), 
		           "Or press R to try again!", smallFont);
	}
}
