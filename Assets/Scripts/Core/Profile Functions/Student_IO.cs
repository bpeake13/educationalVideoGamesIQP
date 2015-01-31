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

	// Encryption
	bool useEncryption = true;

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

	// Citation: http://stackoverflow.com/questions/7590446/set-file-permissions-in-c-sharp
	private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
	{
		return attributes & ~attributesToRemove;
	}

	// Exports student data to the given folder
	public void Export(string in_filepath, Student_Data data, bool encryption = true) {
		string tname = data.s_name;
		string f_filepath = in_filepath + @tname + ".txt";
		// Set file attributes so it can be wrote to
		if(File.Exists (f_filepath)) {
			FileAttributes attributes = File.GetAttributes(f_filepath);
			if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
			{
				attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
				File.SetAttributes(f_filepath, attributes);
			}
		}
		// Export the xml using encryption
		var serializer = new XmlSerializer(typeof(Student_Data));
		if(!useEncryption || !encryption) {
			var stream = new FileStream(f_filepath, FileMode.Create);
			serializer.Serialize(stream, data);
			stream.Close();
		} else {
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.CloseOutput = true;
			StringBuilder sb = new StringBuilder();
			using (StringWriter writer = new StringWriter(sb)) {
				serializer.Serialize(writer, data);
				writer.Close ();
			}
			var desEncryption = new DESEncryption();
			string encryptedText = desEncryption.Encrypt(sb.ToString(), "password");
			Write (f_filepath, encryptedText);
			// Set file attributes to read only
			FileAttributes attributes = File.GetAttributes(f_filepath);
			File.SetAttributes(f_filepath, File.GetAttributes(f_filepath) | FileAttributes.ReadOnly);
		}
	}

	// Imports student data from a given folder
	public Student_Data Import(string in_filepath, string password) {
		var serializer = new XmlSerializer(typeof(Student_Data));
		FileStream stream;
		Student_Data data;
		// Set file attributes so it can be wrote to
		if(File.Exists (in_filepath)) {
			FileAttributes attributes = File.GetAttributes(in_filepath);
			if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
			{
				attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
				File.SetAttributes(in_filepath, attributes);
			}
		}
		// Wop Wop Import stuff
		if (System.IO.File.Exists(in_filepath)) {
			stream = new FileStream(in_filepath, FileMode.Open);
			stream.Close();
			if(!useEncryption) {
				data = serializer.Deserialize(stream) as Student_Data;
			} else {
				var desEncryption = new DESEncryption();
				string decryptedData;
				string fileContents;
				Load (in_filepath, out fileContents);
				desEncryption.TryDecrypt(fileContents, "password", out decryptedData);
				using (TextReader textReader = new StringReader(decryptedData))
				{
					data = serializer.Deserialize(textReader) as Student_Data;
				}
				Debug.Log (decryptedData);
				Debug.Log("Decrypted Data Above");
			}
			Debug.Log("Student Data exists and loaded");
		} else {
			// If file does not exists create a new student data object
			data = new Student_Data();
			data.password = password;
			//Export (in_filepath, data);
			Debug.Log("Student Data does not exist. New Student Data created");
		}
		//if(pass
		return data;
	}

	// Exports text to the given folder
	// NOTE: This function may not be compatible with the net player
	public void Write(string in_filepath, string text) {

		//System.IO.File.WriteAllText(in_filepath, text);

		// Write the stream contents to a new file named "AllTxtFiles.txt".
		using (StreamWriter outfile = new StreamWriter(in_filepath))
		{
			outfile.Write(text);
			outfile.Close ();
		}
	}
	
	// Loads a file, then reads it by line and prints the inforrmation
	public bool Load(string fileName, out string filestring)
	{
		filestring = "";
		// Handle any problems that might arise when reading the text
		try {
			string line;
			// Create a new StreamReader
			StreamReader reader = new StreamReader(fileName, Encoding.Default);

			using (reader)
			{
				// While there's lines left in the text file, do this:
				do {
					line = reader.ReadLine();
					if (line != null) {
						// Do stuff with the information, parse info
						//string[] entries = line.Split(',');
						//if (entries.Length > 0) {
							Debug.Log (line);
							filestring = line;
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

