using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("Student_Data")]
public class Student_Data {

	//[XmlAttribute("name")]
	public string s_name = "No_Name";
	public float bestScore = 0f;
	public float totalScore = 0f;
	public float meanScore = 0f;
	[XmlArray("Monsters")]
	[XmlArrayItem("Monster")]
	public List<Student_Data> allStudentData = new List<Student_Data>();
	public int attempts = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
}
