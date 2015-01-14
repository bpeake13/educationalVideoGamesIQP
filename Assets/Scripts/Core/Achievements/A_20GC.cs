using UnityEngine;
using System.Collections;

public class A_20GC : Achievement {
	
	Student_Data sd;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	
	// Use this for initialization
	public A_20GC () {
		description = "Complete twenty games";
		// Student Profile script
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		sd = spscript.getStudentData();
	}
	
	public override bool checkUnlockCondition() {
		if(sd.attempts >= 20) {
			return true;
		}
		return false;
	}
}