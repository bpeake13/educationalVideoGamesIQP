using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats_Graph : MonoBehaviour {

	// See: http://catlikecoding.com/unity/tutorials/graphs/

	// This class generates a graph in the center of the screen using student stats

	private string oldName = "";
	private int resolution = 800;
	private float xOffset = -4.4f;
	private int yOffset = -2;
	private float xSize = 8.8f;
	private int ySize = 4;
	private int maxValue = 0;
	private int minValue = 999999;

	Student_Data sd;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;

	private List<int> graphValues = new List<int>();
	private bool graphDisplayed = false;
	public GUIStyle font;
	public GUIStyle centeredFont;
	
	private ParticleSystem.Particle[] points;

	// Use this for initialization
	void Start () {
		// Student Profile script
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		sd = spscript.getStudentData();
		oldName = sd.s_name;
		initGraph ();
		graphDisplayed = false;
	}

	void initGraph() {
		maxValue = 0;
		minValue = 999999;
		graphValues.Clear ();
		// Add values to graph
		for(int i = 0; i < sd.allScores.Count; i++) {
			graphValues.Add ((int)sd.allScores[i]);
		}
		for(int i = 0; i < graphValues.Count; i++) {
			if(graphValues[i] > maxValue) {
				maxValue = graphValues[i];
			}
			if(graphValues[i] < minValue) {
				minValue = graphValues[i];
			}
		}
		CreatePoints();
	}

	// Set the point data for the visual representation of the graph
	public void CreatePoints() {
		if(graphValues.Count <= 1) {
			return;
		}
		points = new ParticleSystem.Particle[resolution];
		float increment = 1f / (resolution - 1);
		for(int j = 1; j < graphValues.Count; j++) {
			for (int i = (j * resolution)/graphValues.Count; i < ((j + 1) * resolution)/graphValues.Count; i++) {
				int previousValue = 0;
				previousValue = graphValues[j - 1];
				float x = (i - (resolution)/graphValues.Count) * increment * xSize * ((graphValues.Count)/((float)graphValues.Count - 1));
				float y = Mathf.Lerp(previousValue, graphValues[j], 
				                     ((i % (int)((float)(resolution/graphValues.Count))))/(float)(resolution/graphValues.Count));
				y = ((y - minValue)/(maxValue - minValue)) * ySize;
				points[i].position = new Vector3(x + xOffset, y + yOffset, 0f);
				points[i].color = new Color(1f, 1f, 1f);
				points[i].size = 0.1f;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		sd = spscript.getStudentData();
		if(sd.s_name != oldName) {
			particleSystem.Clear ();
			initGraph ();
			oldName = sd.s_name;
		}
		if(graphDisplayed && graphValues.Count > 1) {
			particleSystem.SetParticles(points, points.Length);
		} else {
			particleSystem.Clear ();
		}
	}

	void OnGUI() {
		if(graphDisplayed) {
			GUI.Label (new Rect (Screen.width/2f - 373, Screen.height/2.6f,100,200), maxValue.ToString(), font);
			GUI.Label (new Rect (Screen.width/2f - 373, Screen.height*3.1f/4,100,50), minValue.ToString(), font);
			GUI.Label (new Rect (Screen.width/2f, Screen.height*3.25f/4,0,50), "Time", centeredFont);
			GUIUtility.RotateAroundPivot(-90, new Vector2(Screen.width/2f - 330, Screen.height*3f/5f));
			GUI.Label (new Rect (Screen.width/2f - 330, Screen.height*3f/5f,0,50), "Score", centeredFont);
		}
	}

	public void toggleGraphDisplayed() {
		graphDisplayed = !graphDisplayed;
	}
}
