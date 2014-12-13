using UnityEngine;
using System.Collections;

public class TestMusicDriver : MusicDriver
{
    void Start()
    {
        LoadSong(testSong);
        Play();
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

    [SerializeField]
    public Song testSong;
}
