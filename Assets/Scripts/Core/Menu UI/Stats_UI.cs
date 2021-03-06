﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Stats_UI : MonoBehaviour {

	Student_Data sd;
	Student_Data originalSD;
	private bool studentVersion = false;
	private int dataNo = 0; // The number of files in the directory, used exclusively
							// in the teacher version
	private int viewingNo = -1; // The number of the current file being viewed (based on directory position),
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
	static private int section = 0; // The stats section
	static private int songSection = 0; // 
	private bool statsDisplayed = true;
	static private List<string> songNames = new List<string>();

	public GUIStyle bigFont;
	public GUIStyle mediumFont;

	// Use this for initialization
	void Start () {
		// Student Profile script
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		sd = spscript.getStudentData();
		originalSD = sd;
		g = GameObject.Find("Graph");
		gscript = (Stats_Graph) g.GetComponent(typeof(Stats_Graph));
		axis = GameObject.Find("Axis");
		axisScript = (Graph_Axis) axis.GetComponent(typeof(Graph_Axis));
		// IO Script
		s_io = GameObject.Find("Student_IO");
		ioscript = (Student_IO) s_io.GetComponent(typeof(Student_IO));
		sectionNames.Add ("General Stats");
		sectionNames.Add ("Score/Time");

		// Get all the song names and their stats for sv
		if(studentVersion) {
			songNames.Clear ();
			for (int i = 0; i < sd.allStats.Count; i++)
			{
				songNames.Add(sd.allStats[i].songName);
			}
		}

		// teacher version things
		if(!studentVersion) {
			string filepath = Application.dataPath;
			foreach (string file in Directory.GetFiles(filepath + @"/Student Data/", "*.txt")) {
				fileNames.Add (file);
				if(file == filepath + @"/Student Data/" + sd.s_name + ".txt") {
					viewingNo = dataNo;
					sd = ioscript.LoadProfile(fileNames[viewingNo], "");
				}
				dataNo++;
			}
		}
		if(viewingNo == -1 && fileNames.Count > 0) {
			viewingNo = 0;
			sd = ioscript.LoadProfile(fileNames[0], "");
		}

		// Get all the song names and their stats for tv
		if(!studentVersion) {
			songNames.Clear ();
			for(int j = 0; j < fileNames.Count; j++) {
				sd = ioscript.LoadProfile (fileNames[j], "");
				for (int i = 0; i < sd.allStats.Count; i++)
				{
					bool test = false;
					for(int k = 0; k < songNames.Count; k++) {
						if(songNames[k] == sd.allStats[i].songName) {
							test = true;
						}
					}
					if(!test) {
						songNames.Add(sd.allStats[i].songName);
					}
				}
			}
		}

		// Finally set profile
		if(fileNames.Count > 0) {
			sd = ioscript.LoadProfile(fileNames[viewingNo], "");
		} else {

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public
	string getSongSection() {
		if(songNames.Count > songSection) {
			Debug.Log ("songSection");
			Debug.Log (songSection);
			return songNames[songSection];
		} else {
			Debug.Log ("songSection");
			Debug.Log (songSection);
			return "ERROR_NO_SONG_FOUND";
		}
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
			// Change profiles
			if (GUI.Button (new Rect (Screen.width - Screen.width/4 - Screen.width/16, Screen.height/3 - 49, Screen.width/16, 48), "->")) {
				viewingNo++;
				if(viewingNo >= dataNo) {
					viewingNo = 0;
				}
				if(fileNames.Count > 0) {
					sd = ioscript.LoadProfile (fileNames[viewingNo], "");
					spscript.setStudentData(sd);
				}
				/*for(int i = 0; i < sd.allStats.Count; i++) {
					if(sd.allStats[i].songName == songNames[songSection]) {
						songSection = i;
					}
				}*/
			}
			if (GUI.Button (new Rect (Screen.width/4, Screen.height/3 - 49, Screen.width/16, 48), "<-")) {
				viewingNo--;
				if(viewingNo < 0) {
					viewingNo = dataNo - 1;
				}
				if(fileNames.Count > 0) {
					sd = ioscript.LoadProfile (fileNames[viewingNo], "");
					spscript.setStudentData(sd);
				}
				/*for(int i = 0; i < sd.allStats.Count; i++) {
					if(sd.allStats[i].songName == songNames[songSection]) {
						songSection = i;
						break;
					}
				}*/
			}
		}
		if(studentVersion) {
			if (GUI.Button (new Rect (10, Screen.height * (9f/10f), 150, 40), "Export to Desktop")) {
				// Export stats to desktop
				string filepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
				Debug.Log (filepath);
				ioscript.Export(filepath + @"/", sd);
			}
			if (GUI.Button (new Rect (Screen.width - 110, Screen.height * (9f/10f), 100, 40), "Go Back")) {
				sd = originalSD;
				spscript.setStudentData(sd);
				// Change to stats screen
				Application.LoadLevel ("Menu2");
			}
		} else {
			if (GUI.Button (new Rect (10, Screen.height * (9f/10f), 150, 40), "Export XML to Desktop")) {
				// Export stats to desktop
				string filepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
				Debug.Log (filepath);
				ioscript.Export(filepath + @"/", sd, false);
			}
			if (GUI.Button (new Rect (Screen.width - 110, Screen.height * (9f/10f), 100, 40), "Go Back")) {
				sd = originalSD;
				spscript.setStudentData(sd);
				// Change to stats screen
				Application.LoadLevel ("Menu2");
			}
			//if (GUI.Button (new Rect (Screen.width - 110, Screen.height * (9f/10f), 100, 40), "Exit")) {
				// Change to stats screen
			//	Application.Quit();
			//}
		}
		// Below: UI Common among all versions
		if (GUI.Button (new Rect (Screen.width - Screen.width/4 - Screen.width/16, Screen.height/3 + 25, Screen.width/16, 24), "->")) {
			section++;
			if(section >= sectionNames.Count) {
				section = 0;
			}
			gscript.toggleGraphDisplayed();
			statsDisplayed = !statsDisplayed;
			axisScript.toggleVisibility();
		}
		if (GUI.Button (new Rect (Screen.width/4, Screen.height/3 + 25, Screen.width/16, 24), "<-")) {
			section--;
			if(section < 0) {
				section = sectionNames.Count - 1;
			}
			gscript.toggleGraphDisplayed();
			statsDisplayed = !statsDisplayed;
			axisScript.toggleVisibility();
		}
		// Toggle song sectons
		if (GUI.Button (new Rect (Screen.width/4, Screen.height/3, Screen.width/16, 24), "<-")) {
			if(songNames.Count > 0) {
				songSection--;
				if(songSection < 0) {
					songSection = songNames.Count - 1;
				}
			}
			spscript.setStudentData(sd);
		}
		if (GUI.Button (new Rect (Screen.width - Screen.width/4 - Screen.width/16, Screen.height/3, Screen.width/16, 24), "->")) {
			if(songNames.Count > 0) {
				songSection++;
				if(songSection >= songNames.Count) {
					songSection = 0;
				}
			}
			spscript.setStudentData(sd);
		}
		GUI.Box (new Rect (Screen.width/4 + Screen.width/16, Screen.height/3 + 25, Screen.width * 3.01f/8f, 24), sectionNames[section]);
		if(songNames.Count > 0) {
			GUI.Box (new Rect (Screen.width/4 + Screen.width/16, Screen.height/3, Screen.width * 3.01f/8f, 24), songNames[songSection]);
			// Display Stats
			if(statsDisplayed) {
				int k = songSection;
				bool nope = false;
				for(int i = 0; i < sd.allStats.Count; i++) {
					if(sd.allStats[i].songName == songNames[songSection]) {
						k = i;
						break;
					}
					if(i == sd.allStats.Count - 1) {
						nope = true;
					}
				}
				if(!nope && k < sd.allStats.Count) {
				GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + Screen.height*(1/9f) + 20f + 30f,300 + 20f,50), "Attempts: " + sd.allStats[k].attempts, mediumFont); // Used to be /8f
				GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + Screen.height*(2/9f) + 20f + 30f,300 + 20f,50), "Best Score: " + sd.allStats[k].bestScore, mediumFont);
				GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + Screen.height*(3/9f) + 20f + 30f,300 + 20f,50), "Mean Score: " + sd.allStats[k].meanScore, mediumFont);
				GUI.Label (new Rect (Screen.width * (1f/8f) + 30, Screen.height/4 + Screen.height*(4/9f) + 20f + 30f,300 + 20f,50), "Total Score: " + sd.allStats[k].totalScore, mediumFont);
				// Column 2
				GUI.Label (new Rect (Screen.width * (4f/8f) + 30, Screen.height/4 + Screen.height*(1/9f) + 20f + 30f,300 + 20f,50), "Most Hits: " + sd.allStats[k].mostHits, mediumFont);
				GUI.Label (new Rect (Screen.width * (4f/8f) + 30, Screen.height/4 + Screen.height*(2/9f) + 20f + 30f,300 + 20f,50), "Most Mobs Slain: " + sd.allStats[k].mostEnemiesDefeated, mediumFont);
				GUI.Label (new Rect (Screen.width * (4f/8f) + 30, Screen.height/4 + Screen.height*(3/9f) + 20f + 30f,300 + 20f,50), "Mean Mobs Slain: " + sd.allStats[k].meanEnemiesDefeated, mediumFont);
				GUI.Label (new Rect (Screen.width * (4f/8f) + 30, Screen.height/4 + Screen.height*(4/9f) + 20f + 30f,300 + 20f,50), "Total Mobs Slain: " + sd.allStats[k].totalEnemiesDefeated, mediumFont);
				}
				//GUI.Label (new Rect (Screen.width * (4f/8f) + 30, Screen.height/4 + 133f + 20f,300 + 20f,50), "More stats to come!", mediumFont);
			}
		} else {
			GUI.Box (new Rect (Screen.width/4 + Screen.width/16, Screen.height/3, Screen.width * 3.01f/8f, 24), "You have played no songs!");
		}
	}
}
