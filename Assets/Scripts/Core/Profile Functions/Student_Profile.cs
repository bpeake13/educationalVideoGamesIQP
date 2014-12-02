using UnityEngine;
using System.Collections;

public class Student_Profile : MonoBehaviour {

	// The currently loaded student data
	private static Student_Data studentData = new Student_Data();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public
	// Get the current;y open student's data
	Student_Data getStudentData() {
		return studentData;
	}

	public
	// Set the current;y open student's data
	void setStudentData(Student_Data input) {
		studentData = input;
	}
}
