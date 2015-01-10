using UnityEngine;
using System.Collections;

public class Graph_Axis : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleVisibility() {
		//renderer.enabled = !renderer.enabled;
		Component[] a = GetComponentsInChildren(typeof(Renderer));
		foreach (Component b in a)
		{
			Renderer r = (Renderer)b;
			r.enabled = !r.enabled;
		}
	}
}
