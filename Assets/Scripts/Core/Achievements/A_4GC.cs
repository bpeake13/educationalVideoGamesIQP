using UnityEngine;
using System.Collections;

public class A_4GC : Achievement {
	
	Student_Data sd;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	
	// Use this for initialization
	public A_4GC () {
		description = "Complete four games";
		// Student Profile script
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		sd = spscript.getStudentData();
	}
	
	public override bool checkUnlockCondition() {
		if(sd.attempts > 3) {
			return true;
		}
		return false;
	}
}