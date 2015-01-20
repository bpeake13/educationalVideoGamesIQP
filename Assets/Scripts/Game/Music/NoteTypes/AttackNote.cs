using UnityEngine;
using System.Collections;

public class AttackNote : Note {
	
	private GameObject mon; // The student profile object
	private Monster monscript;
	[SerializeField]
	private int attack;

	// Use this for initialization
	void Start () {
		// Monster script
		mon = GameObject.Find("Monsters");
		if(mon != null) {
			monscript = (Monster) mon.GetComponent(typeof(Monster));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void Execute ()
	{
		if(mon != null) {
			monscript.onHit(attack);
		}
	}
	
}
