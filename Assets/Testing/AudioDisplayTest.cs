using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WaveformDisplay))]
public class AudioDisplayTest : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        //WaveformDisplay display = GetComponent<WaveformDisplay>();
        //display.Load(clip);
	}

    [SerializeField]
    private AudioClip clip;
}
