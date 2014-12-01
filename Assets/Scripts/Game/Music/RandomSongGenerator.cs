using UnityEngine;
using System.Collections;

public class RandomSongGenerator : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        lib = FindObjectOfType<NoteTypeLib>();

        AudioClip clip = song.Clip;

        float timeLength = clip.length / 60f;



	}

    private NoteTypeLib lib;

    [SerializeField]
    private Song song;
}
