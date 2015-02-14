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
			string grs = PlayerPrefs.GetString( "GameReturnScreen" );
			// Update the student metrics if one did not come from the editor
			if(grs == "Menu2") {
				// Update student data
				UpdateStudentMetrics ();
				// Export the data
				IOScript.Export(filepath, studentData);
			}
			// Goto title screen or editor based on previous screen
			Application.LoadLevel( grs );
		}
	}

	void UpdateStudentMetrics() {
		studentData.attempts++;
		int songSection = 0;
		bool trigger = false;
		string currentSong = SongLoader.Instance.GetSong().Name;
		for(int i = 0; i < studentData.allStats.Count; i++) {
			if(studentData.allStats[i].songName == currentSong) {
				songSection = i;
				trigger = true;
				break;
			}
		}
		if(!trigger) {
			Song_Stats stats = new Song_Stats();
			stats.songName = currentSong;
			songSection = studentData.allStats.Count;
			studentData.allStats.Add(stats);
		}
		if(gmscript.score > studentData.allStats[songSection].bestScore) {
			studentData.allStats[songSection].bestScore = gmscript.score;
		}
		if(gmscript.hits > studentData.allStats[songSection].mostHits) {
			studentData.allStats[songSection].mostHits = gmscript.hits;
		}
		if(gmscript.enemiesDefeated > studentData.allStats[songSection].mostEnemiesDefeated) {
			studentData.allStats[songSection].mostEnemiesDefeated = gmscript.enemiesDefeated;
		}
		if(gmscript.misses < studentData.allStats[songSection].fewestMisses) {
			studentData.allStats[songSection].fewestMisses = gmscript.misses;
		}
		Song_Score ss = new Song_Score();
		ss.song_name = currentSong;
		ss.score = gmscript.score;
		studentData.songScores.Add (ss);
		studentData.allStats[songSection].attempts += 1;
		studentData.allStats[songSection].totalScore += gmscript.score;
		studentData.allStats[songSection].totalEnemiesDefeated += gmscript.enemiesDefeated;
		studentData.allStats[songSection].meanScore = studentData.allStats[songSection].totalScore/studentData.attempts;
		studentData.allStats[songSection].meanEnemiesDefeated = studentData.allStats[songSection].totalEnemiesDefeated/(float)studentData.attempts;
	}
}
