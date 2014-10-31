using UnityEngine;
using System.Collections;
using System.IO;

public class Activity : MonoBehaviour
{
	/// <summary>
	/// Run this activity
	/// </summary>
	public void Run()
	{

	}

	public void Serialize(BinaryWriter writer)
	{
		writer.Write(name);
		writer.Write (description);
		writer.Write (sceneName);
		writer.Write (studentID);
	}

	public void Deserailize(BinaryReader reader)
	{
		name = reader.ReadString();
		description = reader.ReadString();
		sceneName = reader.ReadString();
		studentID = reader.ReadString();
	}

	[SerializeField]
	[Tooltip("The name of this activity")]
	private string name;//the name of this activity

	[SerializeField]
	[Tooltip("The description for this activity")]
	private string description;

	[SerializeField]
	[Tooltip("The identifier that unity will use to load the scene")]
	private string sceneName;

	[SerializeField]
	[Tooltip("The identifier of which student this is, if empty this means it is a template activity file")]
	private string studentID;
}
