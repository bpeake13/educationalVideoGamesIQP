using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {

	private Counter health = new Counter(); // The health of the mob
	private Counter accumulater = new Counter();
	private bool isActive = true; // Whether the mob will be targeted in attacks
	private string enemyType; // The type of the enemy
	private AudioSource sword;

	Student_Data studentData;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	
	private GameObject gm; // The game metric object
	private Game_Metric gmscript;

	private static List<string> allTypes = new List<string>(); 

	// NOTE: All monster types are handled from this one class right now, consider using sub classes

	private static Monster monsters;

	public static Monster Instance
	{
		get {
			if (monsters) {
				return monsters;
			}
			
			GameObject obj = new GameObject("Monster", typeof(Monster));
			monsters = obj.GetComponent<Monster>();
			monsters.Initialization();
			
			return monsters;
		}
	}
	
	// Use this for initialization
	void Initialization () {
		// Init enemy health
		health.setMaxValue(Random.Range (6, 12));
		health.setValue(100);
		Debug.Log ("enemy health set to " + health.getValue());
		// Init sword sound effect
		sword = gameObject.AddComponent<AudioSource>();
		sword.clip = Resources.Load("sword_clash") as AudioClip;
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

	public int getAccumulaterValue() {
		return accumulater.getValue();
	}

	public string getEnemyType() {
		return enemyType;
	}

	public Counter getEnemyHealth() {
		return health;
	}
}
