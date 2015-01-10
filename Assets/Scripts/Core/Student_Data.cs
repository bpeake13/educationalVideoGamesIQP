using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("Student_Data")]
public class Student_Data {
	
	public string s_name = "No_Name";
	public float bestScore = 0f;
	public float totalScore = 0f;
	public float meanScore = 0f;
	public int mostHits = 0;
	public int fewestMisses = 999;
	public string password = "";
	public int attempts = 0;
	[XmlArray("Student_Data")]
	[XmlArrayItem("Student_Data")]
	public List<float> allScores = new List<float>();

	// Use this for initialization
	void Start () {
		allScores.Add (0);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
