using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unlocks_UI : MonoBehaviour {

	public Texture texture1;
	public Texture texture2;
	private bool passwordFailed = false;
	private List<Achievement> achievements = new List<Achievement>();
	int numUnlocked = 0;
	int numUnlockedAvatars = 0;
	public List<Texture> allAvatars = new List<Texture>();
	static public List<bool> unlockedAvatars = new List<bool>();
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
		achievements.Add (new A_1GC());
		achievements.Add (new A_4GC());
		achievements.Add (new A_10GC());
		achievements.Add (new A_20GC());
		for(int i = 0; i < allAvatars.Count; i++) {
			if(sd.unlockedAvatars.Count < allAvatars.Count) {
				if(i == 0 && sd.unlockedAvatars.Count == 0) {
					sd.unlockedAvatars.Add (true);
				} else {
					sd.unlockedAvatars.Add (false);
				}
			}
		}
		for(int i = 0; i < sd.unlockedAvatars.Count; i++) {
			if(sd.unlockedAvatars[i]) {
				numUnlockedAvatars++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown ("enter")) {
			Proceed ();
		}
		numUnlocked = 0;
		for(int i = 0; i < achievements.Count; i++) {
			if(achievements[i].getUnlockState()) {
				numUnlocked++;
			}
		}
		sd.achievementsUnlocked = numUnlocked;
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
		GUI.Box(new Rect(Screen.width/2, Screen.height/4 + 30, Screen.width*3/8, Screen.height/4), "");
		GUI.Box(new Rect(Screen.width/2, Screen.height/2 + 45, Screen.width*3/8, Screen.height/4), "");
		for(int i = 0; i < achievements.Count; i++) {
			GUI.Box(new Rect(Screen.width/8, Screen.height/4 + i * 80 + 30, Screen.width*3/8 - 10, Screen.height/8), "");
			GUI.Label (new Rect(Screen.width/8 + 60, Screen.height/4 + i * 80 + 30, Screen.width*1/4, Screen.height/8), achievements[i].getDescription(), achievementFont);
			if(achievements[i].getUnlockState()) {
				GUI.Box(new Rect(Screen.width/8 + 10, Screen.height/4 + i * 80 + 30 + 20, 40, 40), texture2);
			} else {
				GUI.Box(new Rect(Screen.width/8 + 10, Screen.height/4 + i * 80 + 30 + 20, 40, 40), texture1);
			}
		}
		//if (GUI.Button (new Rect (Screen.width/2 -  50, Screen.height/2 + 50, 100, 20), "Continue")) {
		//	Proceed();
		//}
		GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/8 + 20,100,50), "Unlock Things!", bigFont);
		GUI.Label (new Rect (Screen.width/2 - 100, Screen.height/8 + 65,200,50), "Acheivements Unlocked: " + numUnlocked + "/" + achievements.Count, stdFont);
		if (GUI.Button (new Rect (Screen.width - 110, Screen.height * (9f/10f), 100, 40), "Go Back")) {
			// Change to stats screen
			Application.LoadLevel ("Menu2");
		}
		// Unlocks
		GUI.Label (new Rect (Screen.width*3/5, Screen.height/3,200,50), "You have " + (sd.achievementsUnlocked - numUnlockedAvatars + 1) + " unlock points", stdFont);
		if (GUI.Button (new Rect (Screen.width*3/5, Screen.height/2.5f, 200, 40), "Get a new random avatar!") && sd.achievementsUnlocked - numUnlockedAvatars + 1> 0) {
			numUnlockedAvatars += 1;
			int rand = Random.Range (0, sd.unlockedAvatars.Count);
			while(true) {
				rand = Random.Range (0, sd.unlockedAvatars.Count);
				if(!sd.unlockedAvatars[rand]) {
					sd.unlockedAvatars[rand] = true;
					//unlockedAvatars[rand] = true;
					// Export the unlock data
					string filepath = Application.dataPath + @"/Student Data/";
					ioscript.Export(filepath, sd);
					break;
				}
			}
		}
		GUI.Label (new Rect (Screen.width*3/5, Screen.height*3/5,200,50), "Click an avatar to use it", stdFont);
		// The avatars
		for(int i = 0; i < 5; i++) {
			if(sd.unlockedAvatars[i]) {
				if(GUI.Button(new Rect(Screen.width*3.5f/5f + (i - 2.5f) * 50, Screen.height*2/3, 40, 40), allAvatars[i])) {
					sd.selectedAvatar = i;
					string filepath = Application.dataPath + @"/Student Data/";
					ioscript.Export(filepath, sd);
				}
			} else {
				GUI.Box(new Rect(Screen.width*3.5f/5f + (i - 2.5f) * 50, Screen.height*2/3, 40, 40), texture1);
			}
		}
	}
}
