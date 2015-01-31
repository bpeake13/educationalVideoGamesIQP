using UnityEngine;
using System.Collections;

public class TestNote : Note {

	private GameObject mon; // The student profile object
	private EnemyManager monscript;

	// Use this for initialization
	void Start () {
		// Monster script
		mon = GameObject.Find("Monsters");
		monscript = (EnemyManager) mon.GetComponent(typeof(EnemyManager));
	}

	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute ()
	{
        monscript.addAccumulater(1);
	}
	
}
