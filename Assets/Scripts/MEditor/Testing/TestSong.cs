using UnityEngine;
using System.Collections;

public class TestSong : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        TrackEditorManager.Instance.Load(testSong);
	}

    [SerializeField]
    private Song testSong;
}
