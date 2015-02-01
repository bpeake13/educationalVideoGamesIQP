using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("Student_Data")]
public class Student_Data {
	
	public string s_name = "No_Name";
	public string password = "";
	public int achievementsUnlocked = 0;
	public int selectedAvatar = 0;
	public int attempts = 0;
	[XmlArray("Song_Stats")]
	[XmlArrayItem("Song_Stats")]
	public List<Song_Stats> allStats = new List<Song_Stats>();
	[XmlArray("Song_Scores")]
	[XmlArrayItem("Song_Scores")]
	public List<Song_Score> songScores = new List<Song_Score>();
	[XmlArray("Avatars_Unlocked")]
	[XmlArrayItem("Avatars_Unlocked")]
	public List<bool> unlockedAvatars = new List<bool>();

}
