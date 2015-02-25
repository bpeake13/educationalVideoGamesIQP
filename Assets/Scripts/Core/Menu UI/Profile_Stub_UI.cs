using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Profile_Stub_UI : MonoBehaviour {

	private bool studentVersion = true; //
	// The currently loaded student data
	Student_Data studentData;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	private List<Texture2D> textures = new List<Texture2D>();

	// Use this for initialization
	void Start () {
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		studentData = spscript.getStudentData();
		textures.Add (Resources.Load<Texture2D>("Face1"));
		textures.Add (Resources.Load<Texture2D>("Face2"));
		textures.Add (Resources.Load<Texture2D>("Face3"));
		textures.Add (Resources.Load<Texture2D>("Face4"));
		textures.Add (Resources.Load<Texture2D>("Face5"));
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI() {
		//if(studentVersion) {
			GUI.Box(new Rect(Screen.width - 220, 10, 190, 50), "");
			//GUI.Box(new Rect(Screen.width/8, Screen.height/8, Screen.width * (3f/4f), Screen.height * (3f/4f)), "");
			GUI.Label (new Rect (Screen.width - 210, 12,390, 50), studentData.s_name);
			GUI.Box(new Rect(Screen.width - 80, 15, 40, 40), textures[studentData.selectedAvatar]);
			if(GUI.Button (new Rect (Screen.width - 210, 32,120, 20), "Change Password")) {
				Application.LoadLevel ("Password");
			}
		//}
	}
}
