using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Student_IO : MonoBehaviour {

	// variables for testing purposes
	//float score;
	//string studentName;
	//string outputText;

	// Use this for initialization
	void Start () {
		Debug.Log("Started!");
		Debug.Log("Getting Student Names... (jk)");
		/*// Following calls / variables are for testing purposes
		studentName = "Sally";
		score = UnityEngine.Random.Range (0f, 100f);
		outputText = studentName + "\r\n" + score;
		string filepath = @"C:\Users\Benjamin\Documents\IQP Project\";
		Load (filepath + @"Student Names.txt");
		foreach (string file in Directory.GetFiles(filepath + @"Student Data\", "*.txt")) {
			string contents = File.ReadAllText(file);
			Debug.Log (contents);
		}
		Write(filepath + @"Results.txt", outputText);
		Student_Data data = new Student_Data();
		data.bestScore = 2;
		data.meanScore = 4;
		Export(filepath + @"Student Data\", data);
		Student_Data d = Import(filepath + @"Student Data\No_Name.txt");
		Debug.Log (d.meanScore);*/
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Exports student data to the given folder
	private void Export(string in_filepath, Student_Data data) {
		string tname = data.s_name;
		string f_filepath = in_filepath + @tname + ".txt";
		var serializer = new XmlSerializer(typeof(Student_Data));
		var stream = new FileStream(f_filepath, FileMode.Create);
		serializer.Serialize(stream, data);
		stream.Close();
	}

	// Imports student data from a given folder
	private Student_Data Import(string in_filepath) {
		var serializer = new XmlSerializer(typeof(Student_Data));
		var stream = new FileStream(in_filepath, FileMode.Open);
		Student_Data data = serializer.Deserialize(stream) as Student_Data;
		stream.Close();
		return data;
	}

	// Exports text to the given folder
	private void Write(string in_filepath, string text) {
		System.IO.File.WriteAllText(in_filepath, text);
	}

	// Loads a file, then reads it by line and prints the inforrmation
	private bool Load(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			string line;
			// Create a new StreamReader
			StreamReader reader = new StreamReader(fileName, Encoding.Default);

			using (reader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = reader.ReadLine();
					
					if (line != null)
					{
						// Do stuff with the information, parse info
						//string[] entries = line.Split(',');
						//if (entries.Length > 0) {
							Debug.Log (line);
						//}
					}
				}
				while (line != null);
				
				// Close the string reader
				reader.Close();
				return true;
			}
		}
		
		// If anything broke in the try block, we throw an exception
		// and give the user an error message
		catch (Exception e)
		{
			Debug.Log ("There was an error loading the file :(");
			return false;
		}
	}
}


// OLD CODE
/*string allMets = "";
for(int i = 0; i < data.allMetrics.Count; i++) {
	allMets += data.allMetrics.ToArray()[i];
	allMets += "\r\n";
}
string all = tname + "\r\n" + allMets;
System.IO.File.WriteAllText(f_filepath, all);*/

