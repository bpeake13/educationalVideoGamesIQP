using UnityEngine;
using System.Collections;

public class Student_Functions : MonoBehaviour {

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
		// Export student data
		string filepath = Application.dataPath + @"/Student Data/";
		IOScript.Export(filepath, studentData);
	}
	
	// Update is called once per frame
	void Update () {
		string filepath = Application.dataPath + @"/Student Data/";
		//if(Input.GetKeyDown ("e")) {
			// Update student data
			//UpdateStudentMetrics ();
			// Export the data
			//IOScript.Export(filepath, studentData);
		//}
		if(Input.GetKeyDown (KeyCode.Escape)) {
			// Update student data
			UpdateStudentMetrics ();
			// Export the data
			IOScript.Export(filepath, studentData);
			// Goto title screen
			Application.LoadLevel ("Menu2");
		}
	}

	void UpdateStudentMetrics() {
		studentData.attempts++;
		if(gmscript.score > studentData.bestScore) {
			studentData.bestScore = gmscript.score;
		}
		if(gmscript.hits > studentData.mostHits) {
			studentData.mostHits = gmscript.hits;
		}
		if(gmscript.misses < studentData.fewestMisses) {
			studentData.fewestMisses = gmscript.misses;
		}
		studentData.allScores.Add(gmscript.score);
		studentData.totalScore += gmscript.score;
		studentData.meanScore = studentData.totalScore/studentData.attempts;
	}
}
