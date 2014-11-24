using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Object that is in charge of driving the music
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MusicDriver : MonoBehaviour
{
    /// <summary>
    /// The song that is currently loaded
    /// </summary>
    /// <value>The loaded song.</value>
    public Song CurrentSong
    {
        get{ return song;}
    }

    void Awake()
    {
        source = audio;
    }

    public void LoadSong(Song song)
    {
        source.clip = song.Clip;//load the audio clip

        this.song = song;
    }

    public void AddMusicPlayer(MusicPlayer player)
    {
        players.Add(player);
        player.Setup(this);
    }

    public void RemoveMusicPlayer(MusicPlayer player)
    {
        players.Remove(player);
    }

    public void Play()
    {
        if(source.clip != song.Clip)
            source.clip = song.Clip;
        isPlaying = true;
        isPaused = false;
    }

    protected virtual void OnBeatStart()
    {
        foreach (MusicPlayer player in players)
        {
            player.OnBeatStart();
        }
    }

    protected virtual void OnBeatEnd()
    {
        foreach (MusicPlayer player in players)
        {
            player.OnBeatEnd();
        }
    }

    private Song song;

    private List<MusicPlayer> players = new List<MusicPlayer>();

    /// <summary>
    /// If true, then the song is playing, if not it has been stopped
    /// </summary>
    private bool isPlaying;
    /// <summary>
    /// If true then the song is paused
    /// </summary>
    private bool isPaused;

    private AudioSource source;
}
