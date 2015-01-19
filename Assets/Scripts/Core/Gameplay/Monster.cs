﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {

	private Counter health = new Counter(); // The health of the mob
	private Counter accumulater = new Counter();
	private float lifeTimer;
	private bool isActive = true; // Whether the mob will be targeted in attacks
	private string enemyType; // The type of the enemy
	private GameObject md; // The music driver object
	private TestMusicDriver mdscript;
	public GUIStyle font;
	public AudioSource sword;

	Student_Data studentData;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	
	private GameObject gm; // The game metric object
	private Game_Metric gmscript;

	private static List<string> allTypes = new List<string>(); 

	public GUIStyle centeredFont;
	public GUIStyle enemyFont;
	public GUIStyle bigFont;
	public GUIStyle FinalFont;

	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	public Texture2D progressBarBorder;
	public GUIStyle barStyle;

	// NOTE: All monster types are handled from this one class right now, consider using sub classes

	// Use this for initialization
	void Start () {
		// Init enemy health
		health.setMaxValue(Random.Range (6, 12));
		health.setValue(100);
		Debug.Log ("enemy health set to " + health.getValue());
		// Music Driver script
		md = GameObject.Find("MusicDriver");
		mdscript = (TestMusicDriver) md.GetComponent(typeof(TestMusicDriver));
		// Student data script
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		studentData = spscript.getStudentData();
		// Game Metric script
		gm = GameObject.Find("GameMetric");
		gmscript = (Game_Metric) gm.GetComponent(typeof(Game_Metric));
		// Init enemy types
		allTypes.Add ("Bananas");
		allTypes.Add ("Your teacher");
		allTypes.Add ("A felt cactus");
		allTypes.Add ("Clingy ghost");
		allTypes.Add ("Bad Candy");
		allTypes.Add ("Something BIG");
		allTypes.Add ("Mandatory zombie");
		allTypes.Add ("Sneaky shellfish");
		allTypes.Add ("Unsticky Tape");
		enemyType = allTypes.ToArray()[Random.Range (0, allTypes.Count)];
	}
	
	// Update is called once per frame
	void Update () {
		lifeTimer += Time.deltaTime;
	}

	// Things that occur when the mob is hit
	public
	void onHit(int damage) {
		if(isActive) {
			health.subtract (damage);
			accumulater.add (damage);
			// Do other things related to animation, scoring, etc. here
			Debug.Log ("hit registered");
			if(health.getValue() <= 0) {
				Debug.Log ("enemy destroyed");
				// Spawn a new "enemy"
				health.setMaxValue(Random.Range (4, 9));
				health.setValue(100);
				enemyType = allTypes.ToArray()[Random.Range (0, allTypes.Count)];
				// Score boost!
				gmscript.score += 90;
				sword.Play ();
			}
		}
	}

	public void releaseAccumulater() {
		accumulater.setValue(0);
		// Then will destroy all enemies with the same value as the accumulater
	}

	// GUI methods related to monsters
	void OnGUI () {
		GUI.Box (new Rect(Screen.width - 190, 10, 180, 35), "");
		GUI.Label (new Rect (Screen.width/2,50,0,50), "Enemy: " + enemyType, enemyFont);
		GUI.Label (new Rect (Screen.width - 180,10,1000,50), "Health: " + health.getValue (), font);
		// TODO: Terrible place for the rest of this
		// Metric based GUI here for now
		GUI.Box (new Rect(10, 10, 180, 70), "");
		GUI.Label (new Rect (20,10,1000,50), "Hits: " + gmscript.hits, font);
		GUI.Label (new Rect (20,45,1000,50), "Misses: " + gmscript.misses, font);
		//GUI.Label (new Rect (20,80,1000,50), "Score: " + gmscript.score, font);
		// Display Accumulater
		GUI.Label (new Rect(Screen.width/2, Screen.height/5 - 16, 1, 1), accumulater.getValue ().ToString(), centeredFont);
		// Offsets
		int offset = 62; // 64
		int off2 = 280; // 212
		int offY = Screen.height/64;
		int offM = Screen.height/12;
		// Button mapping here for now
		GUI.Label (new Rect (50,Screen.height/2 - offM*1.5f + offY,1000,0), "A", bigFont);
		GUI.Label (new Rect (50,Screen.height/2 - offM/2 + offY,1000,0), "S", bigFont);
		GUI.Label (new Rect (50,Screen.height/2 + offM/2 + offY,1000,0), "D", bigFont);
		GUI.Label (new Rect (50,Screen.height/2 + offM*1.5f + offY,1000,0), "F", bigFont);
		// Final score screen
		if(mdscript.CurrentSong.Clip.length + 5 < lifeTimer) {
			GUI.Box(new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), "");
			GUI.Label (new Rect(Screen.width/4, Screen.height/2 - 40, Screen.width/2, Screen.height/2), 
			           "COMPLETE", FinalFont);
			if((int)(Time.time*2) % 2 == 0) {
				GUI.Label (new Rect(Screen.width/4, Screen.height/2 + 20, Screen.width/2, Screen.height/2), 
			           "Press ESC to return", FinalFont);
			}
		}
		// draw the background:
		//ScaleMode.StretchToFill;
		GUI.BeginGroup (new Rect (Screen.width/4 - 3, 20 - 3, Screen.width/2 + 6, 20 + 6));
		GUI.DrawTexture (new Rect (0,0, Screen.width/2 + 6, 26),progressBarBorder, ScaleMode.StretchToFill );
		GUI.DrawTexture (new Rect (3,3, Screen.width/2, 20),progressBarEmpty, ScaleMode.StretchToFill );
			
			// draw the filled-in part:
			float barDisplay = health.getValue ()/(float)health.getMaxValue();
			GUI.BeginGroup (new Rect (3, 3, Screen.width/2 * barDisplay, 20));
			GUI.DrawTexture (new Rect (0,0, Screen.width/2, 20),progressBarFull, ScaleMode.StretchToFill );
			
			GUI.EndGroup ();

		GUI.EndGroup ();
			
	}
}
