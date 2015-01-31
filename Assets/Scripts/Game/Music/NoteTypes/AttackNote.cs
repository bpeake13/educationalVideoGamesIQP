using UnityEngine;
using System.Collections;

public class AttackNote : Note {

	[SerializeField]
	private int attack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void Execute ()
	{
        EnemyManager.Instance.addAccumulater(attack);
	}
	
}
