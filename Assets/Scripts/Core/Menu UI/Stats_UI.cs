using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Stats_UI : MonoBehaviour {

	Student_Data sd;
	private bool studentVersion = true;
	private int dataNo = 0; // The number of files in the directory, used exclusively
							// in the teacher version
	private int viewingNo = 0; // The number of the current file being viewed (based on directory position),
							   // used exclusively in the teacher version
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	private GameObject g; // The graph object
	private Stats_Graph gscript;
	private GameObject axis; // The axis object
	private Graph_Axis axisScript;
	private GameObject s_io; // IO
	private Student_IO ioscript;
	private List<string> sectionNames = new List<string>();
	private List<string> fileNames = new List<string>();
	private int section = 0;
	private bool statsDisplayed = true;

	public GUIStyle bigFont;
	public GUIStyle mediumFont;

	// Use this for initialization
	void Start () {
		// Student Profile script
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		sd = spscript.getStudentData();
		g = GameObject.Find("Graph");
		gscript = (Stats_Graph) g.GetComponent(typeof(Stats_Graph));
		axis = GameObject.Find("Axis");
		axisScript = (Graph_Axis) axis.GetComponent(typeof(Graph_Axis));
		// IO Script
		s_io = GameObject.Find("Student_IO");
		ioscript = (Student_IO) s_io.GetComponent(typeof(Student_IO));
		sectionNames.Add ("General Stats");
		sectionNames.Add ("Score/Time");
		// teacher version things
		if(!studentVersion) {
			string filepath = Application.dataPath;
			foreach (string file in Directory.GetFiles(filepath + @"\Student Data\", "*.txt")) {
				fileNames.Add (file);
				if(file == filepath + @"\Student Data\" + sd.s_name + ".txt") {
					viewingNo = dataNo;
					sd = ioscript.Import (fileNames[viewingNo], "");
				}
				dataNo++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
		GUI.Box(new Rect(Screen.width/8, Screen.height/8, Screen.width * (3f/4f), Screen.height * (3f/4f)), "");
		if(studentVersion) {
			GUI.Label (new Rect (Screen.width/2 - 150, Screen.height/6,300,49), "Hi, " + sd.s_name, bigFont);
			GUI.Label (new Rect (Screen.width/2 - 150, Screen.height/6 + 50,300,49), "This is your progress so far...", bigFont);
		} else {
			GUI.Label (new Rect (Screen.width/2 - 150, Screen.height/6,300,49), "Viewing student data for", bigFont);
			GUI.Box (new Rect (Screen.width/4 + Screen.width/16, Screen.height/3 - 49, Screen.width * 3.01f/8f, 48), "");
			GUI.Label (new Rect (Screen.width/4 + Screen.width/16, Screen.height/3 - 49, Screen.width * 3.01f/8f, 48), sd.s_name, bigFont);
			if (GUI.Button (new Rect (Screen.width - Screen.width/4 - Screen.width/16, Screen.height/3 - 49, Screen.width/16, 48), "->")) {
				viewingNo++;
				if(viewingNo >= dataNo) {
					viewingNo = 0;
				}
				sd = ioscript.Import (fileNames[viewingNo], "");
				spscript.setStudentData(sd);
			}
			if (GUI.Button (new Rect (Screen.width/4, Screen.height/3 - 49, Screen.width/16, 48), "<-")) {
				viewingNo--;
				if(viewingNo < 0) {
					viewingNo = dataNo - 1;
				}
				sd = ioscript.Import (fileNames[viewingNo], "");
				spscript.setStudentData(sd);
			}
		}
		if(studentVersion) {
			if (GUI.Button (new Rect (10, Screen.height * (9f/10f), 150, 40), "Export to Desktop")) {
				// Export stats to desktop
				string filepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
				Debug.Log (filepath);
				ioscript.Export(filepath + @"\", sd);
			}
			if (GUI.Button (new Rect (Screen.width - 110, Screen.height * (9f/10f), 100, 40), "Go Back")) {
				// Change to stats screen
				Application.LoadLevel ("Menu2");
			}
		} else {
			if (GUI.Button (new Rect (Screen.width - 110, Screen.height * (9f/10f), 100, 40), "Exit")) {
				// Change to stats screen
				Application.Quit();
			}
		}
		// Below: UI Common among all versions
		if (GUI.Button (new Rect (Screen.width - Screen.width/4 - Screen.width/16, Screen.height/3, Screen.width/16, 24), "->")) {
			section++;
			if(section >= sectionNames.Count) {
				section = 0;
			}
			gscript.toggleGraphDisplayed();
			statsDisplayed = !statsDisplayed;
			axisScript.toggleVisibility();
		}
		if (GUI.Button (new Rect (Screen.width/4, Screen.height/3, Screen.width/16, 24), "<-")) {
			section--;
			if(section < 0) {
				section = sectionNames.Count - 1;
			}
			gscript.toggleGraphDisplayed();
			statsDisplayed = !statsDisplayed;
			axisScript.toggleVisibility();
		}
		GUI.Box (new Rect (Screen.width/4 + Screen.width/16, Screen.height/3, Screen.width * 3.01f/8f, 24), sectionNames[section]);
		// Display Stats
		if(statsDisplayed) {
			GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + 66f + 20f,300 + 20f,50), "Attempts: " + sd.attempts, mediumFont);
			GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + 133f + 20f,300 + 20f,50), "Best Score: " + sd.bestScore, mediumFont);
			GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + 200f + 20f,300 + 20f,50), "Mean Score: " + sd.meanScore, mediumFont);
			GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + 266f + 20f,300 + 20f,50), "Total Score: " + sd.totalScore, mediumFont);
			// Column 2
			GUI.Label (new Rect (Screen.width * (4f/8f) + 30, Screen.height/4 + 66f + 20f,300 + 20f,50), "Most Hits: " + sd.mostHits, mediumFont);
			//GUI.Label (new Rect (Screen.width * (4f/8f) + 30, Screen.height/4 + 133f + 20f,300 + 20f,50), "More stats to come!", mediumFont);
		}
	}
}
