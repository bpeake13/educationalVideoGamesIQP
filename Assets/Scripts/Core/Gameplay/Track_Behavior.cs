using UnityEngine;
using System.Collections;

public class Track_Behavior : MonoBehaviour {

	private Object[] rows;
	public AudioSource badTone;

	private GameObject gm; // The student profile object
	private Game_Metric gmscript;

	// Use this for initialization
	void Start () {
		// Game Metric script
		gm = GameObject.Find("GameMetric");
		gmscript = (Game_Metric) gm.GetComponent(typeof(Game_Metric));
	}
	
	// Update is called once per frame
	void Update () {
		rows = GameObject.FindGameObjectsWithTag("Row");
		bool totalMiss = true;
		foreach (GameObject row in rows) {
			Row Trow = (Row)row.GetComponent(typeof(Row));
			if(Trow.ExecuteInput()) {
				totalMiss = false;
			}
		}
		if(GameObject.FindGameObjectsWithTag("Row").Length == 0) {
			totalMiss = false;
		}
		if(totalMiss) {
			// Whoops, missed the beat
			gmscript.misses += 1;
			gmscript.score -= 10;
			badTone.Play ();
		}
	}
}
