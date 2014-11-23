using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    public void LoadSong(Song song, int track)
    {
        this.song = song;
        this.track = song.GetTrack(track);
    }

    public void Step(float time)
    {
    }

    public void OnBeatStart()
    {
    }

    public void OnBeatEnd()
    {
    }

    private Song song;
    private Track track;

    private float bpm;
}
