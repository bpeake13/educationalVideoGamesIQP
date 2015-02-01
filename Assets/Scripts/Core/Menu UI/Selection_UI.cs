using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Selection_UI : MonoBehaviour {

	public GUIStyle bigFont;

	// The currently loaded student data
	private Student_Data studentData;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;

	// Use this for initialization
	void Start () {
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		studentData = spscript.getStudentData();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		if(bHideGui)
			return;

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
			// Load level
			ViewSongs();
			//Application.LoadLevel ("Game");
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

	public void ViewSongs()
	{
		string directoryName = @"Songs";
		DirectoryInfo songDirectories = new DirectoryInfo(directoryName);
		
		DirectoryInfo[] songs = songDirectories.GetDirectories();
		List<string> names = new List<string>();
		
		int songDirectoryCount = songs.Length;
		for (int i = 0; i < songDirectoryCount; i++)
		{
			DirectoryInfo songDirectory = songs[i];
			string songFile = Path.Combine(songDirectory.FullName, "info.song");
			
			if (!File.Exists(songFile))
				continue;
			
			names.Add(songDirectory.Name);
		}
		
		string[] namesArray = names.ToArray();
		bHideGui = true;
		SelectorDialogue.Show(namesArray, OnSongSelected, "Select A Song", "No songs created yet, select [NEW] instead.");
	}

	void OnSongSelected(string song)
	{
		bHideGui = false;
		if (string.IsNullOrEmpty(song))
			return;
		
		bool couldLoad = SongLoader.Instance.Load(song);

		if (couldLoad)
			Application.LoadLevel("Game");

		// Add the song to the list of student data
		Song_Stats sd = new Song_Stats();
		sd.songName = song;
		studentData.allStats.Add(sd);
	}

	private bool bHideGui = false;
}
