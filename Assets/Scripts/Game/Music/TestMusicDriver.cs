﻿using UnityEngine;
using System.Collections;

public class TestMusicDriver : MusicDriver
{
    void Start()
    {
		//testSong = new Song();
		//testSong.BPM = 120;
		//testSong.BeatPeriod = 
		if(SongLoader.Instance.GetSong() == null) {
			if(SongLoader.Instance.Load ("Test")) {
				Debug.Log ("Song loaded");
			} else {
				Debug.Log ("Song failed to load");
			}
		} else {
			testSong = SongLoader.Instance.GetSong();
			LoadSong(SongLoader.Instance.GetSong());
		}
    }

	public void Activate() {	
		isActive = true;
		Play();
	}

	public bool getActiveStatus() {
		return isActive;
	}

    protected override void OnBeatStart()
    {
        base.OnBeatStart();

        beatCount++;
		// Commenting this out for my own tests right now ^^
        //Debug.Log(beatCount.ToString() + " " + beatTimer.ToString());
        beatTimer = 0;
    }

    protected override void OnBeatEnd()
    {
        base.OnBeatEnd();
    }

    protected override void Update()
    {
        base.Update();

        beatTimer += Time.deltaTime;
    }

    private int beatCount;

    private float beatTimer;

	private bool isActive = false;

    private Song testSong;
}
