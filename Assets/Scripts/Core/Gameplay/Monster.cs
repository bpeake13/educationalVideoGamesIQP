using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {

	private Counter health = new Counter(); // The health of the mob
	private bool isActive = true; // Whether the mob will be targeted in attacks
	private string enemyType; // The type of the enemy
	private GameObject md; // The music driver object
	private TestMusicDriver mdscript;
	private Game_Metric testMetric = new Game_Metric();

	private static List<string> allTypes = new List<string>(); 

	// NOTE: All monster types are handled from this one class right now, consider using sub classes

	// Use this for initialization
	void Start () {
		health.setMaxValue(Random.Range (4, 9));
		health.setValue(100);
		Debug.Log ("enemy health set to " + health.getValue());
		md = GameObject.Find("MusicDriver");
		mdscript = (TestMusicDriver) md.GetComponent(typeof(TestMusicDriver));
		//mdscript.Play();
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
		// Update things
		// Check if on beat
		if(Input.GetKeyDown ("space")) {
			if(mdscript.GetBeatEdge()) {
				testMetric.hits += 1;
				onHit (1);
			} else {
				// Whoops, missed the beat
				testMetric.misses += 1;
				// Score minus :(
				testMetric.score -= 5;
				Debug.Log ("missed the beat");
			}
		}
	}

	// Things that occur when the mob is hit
	void onHit(int damage) {
		if(isActive) {
			health.subtract (damage);
			// Score boost!
			testMetric.score += 10;
			// Do other things related to animation, scoring, etc. here
			Debug.Log ("hit registered");
			if(health.getValue() <= 0) {
				Debug.Log ("enemy destroyed");
				// Spawn a new "enemy"
				health.setMaxValue(Random.Range (4, 9));
				health.setValue(100);
				enemyType = allTypes.ToArray()[Random.Range (0, allTypes.Count)];
				// Score boost!
				testMetric.score += 90;
			}
		}
	}

	// GUI methods related to monsters
	void OnGUI () {
		GUI.Label (new Rect (10,5,1000,50), "Enemy: " + enemyType);
		GUI.Label (new Rect (10,20,1000,50), "Health: " + health.getValue ());
		// Metric based GUI here for now
		GUI.Label (new Rect (200,5,1000,50), "Hits: " + testMetric.hits);
		GUI.Label (new Rect (200,20,1000,50), "Misses: " + testMetric.misses);
		GUI.Label (new Rect (200,35,1000,50), "Score: " + testMetric.score);
	}
}
