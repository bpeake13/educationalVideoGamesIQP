using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {

	private Counter accumulater = new Counter(); // The amount of stored attack
	private bool isActive = true; // Whether the mob will be targeted in attacks
	private AudioSource sword;

	Student_Data studentData;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	
	private GameObject gm; // The game metric object
	private Game_Metric gmscript;

	private static List<string> allTypes = new List<string>(); 
	private static List<Enemy> allEnemies = new List<Enemy>(); 
	private List<Enemy> currentEnemies = new List<Enemy>();

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

		currentEnemies.Add(newEnemy(Random.Range (6, 12), allTypes.ToArray()[Random.Range (0, allTypes.Count)]));
	}

	Enemy newEnemy(int health, string type) {
		Enemy en = new Enemy();
		en.setType(type);
		en.getHealth().setMaxValue(health);
		en.getHealth().setValue(health);
		//allEnemies.Add (new Enemy());
		return en;
	}

	// Update is called once per frame
	void Update () {

	}

	// Things that occur when the mob is hit
	public
	void onHit(int damage) {
		if(isActive) {
			currentEnemies[0].getHealth().subtract (damage);
			accumulater.add (damage);
			// Do other things related to animation, scoring, etc. here
			Debug.Log ("hit registered");
			if(currentEnemies[0].getHealth().getValue() <= 0) {
				Debug.Log ("enemy destroyed");
				// Spawn a new "enemy"
				currentEnemies.Clear ();
				currentEnemies.Add (newEnemy(Random.Range (6, 12), allTypes.ToArray()[Random.Range (0, allTypes.Count)]));
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

	public string getEnemyType(int index) {
		return currentEnemies[index].getType();
	}

	public Counter getEnemyHealth(int index) {
		return currentEnemies[index].getHealth();
	}

	public List<Enemy> getAllCurrentEnemies() {
		return currentEnemies;
	}
}
