﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {

	private Counter health = new Counter(); // The health of the mob
	private bool isActive = true; // Whether the mob will be targeted in attacks
	private string enemyType; // The type of the enemy
	private GameObject md; // The music driver object
	private TestMusicDriver mdscript;

	Student_Data studentData;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	
	private GameObject gm; // The student profile object
	private Game_Metric gmscript;

	private static List<string> allTypes = new List<string>(); 

	public GUIStyle bigFont;

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
		allTypes.Add ("Mandatory Zombie");
		enemyType = allTypes.ToArray()[Random.Range (0, allTypes.Count)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Things that occur when the mob is hit
	public
	void onHit(int damage) {
		if(isActive) {
			health.subtract (damage);
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
			}
		}
	}

	// GUI methods related to monsters
	void OnGUI () {
		GUI.Label (new Rect (10,5,1000,50), "Enemy: " + enemyType);
		GUI.Label (new Rect (10,20,1000,50), "Health: " + health.getValue ());
		// TODO: Terrible place for the rest of this
		// Metric based GUI here for now
		GUI.Label (new Rect (200,5,1000,50), "Hits: " + gmscript.hits);
		GUI.Label (new Rect (200,20,1000,50), "Misses: " + gmscript.misses);
		GUI.Label (new Rect (200,35,1000,50), "Score: " + gmscript.score);
		int offset = 64;
		// Button mapping here for now
		GUI.Label (new Rect (50,192,1000,50), "A", bigFont);
		GUI.Label (new Rect (50,192 + offset,1000,50), "S", bigFont);
		GUI.Label (new Rect (50,192 + offset * 2,1000,50), "D", bigFont);
		GUI.Label (new Rect (50,192 + offset * 3,1000,50), "F", bigFont);
	}
}
