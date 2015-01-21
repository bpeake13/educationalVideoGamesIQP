using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private string type;
	private Counter health = new Counter();

	// Use this for initialization
	void Start () {
		type = "Unknown";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setType(string input) {
		type = input;
	}

	public string getType() {
		return type;
	}

	public Counter getHealth() {
		return health;
	}
}
