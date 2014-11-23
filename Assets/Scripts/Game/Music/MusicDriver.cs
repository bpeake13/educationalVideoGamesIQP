using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Object that is in charge of driving the music
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MusicDriver : MonoBehaviour
{
    void Awake()
    {
        source = audio;
    }

    public void LoadSong(Song song)
    {
        source.clip = song.Clip;//load the audio clip
        
        playDelayTime = song.StartDelay;
        beatOffsetTime = song.BeatOffset;
        secondsPerBeat = song.BeatPeriod;
        
        beatRolloffPeriod = secondsPerBeat * beatRolloff;

        this.song = song;
    }

    public void AddMusicPlayer(MusicPlayer player)
    {
        players.Add(player);
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

    protected virtual void Update()
    {
        if (!isPlaying)
            return;

        if (isPaused)
            return;

        float deltaTime = Time.deltaTime;
        playingTime += deltaTime;//increase the timer by the amount of time that passed

        if (!source.isPlaying)
        {
            //if (playingTime >= playDelayTime)
                //source.Play();
        } 
        else
        {

        }

        float closestBeatTime = GetActiveBeatTime();

        float beatDistance = Mathf.Abs(playingTime - closestBeatTime);//get the distance to the closest beat

        if (beatEdge && beatDistance >= beatRolloffPeriod)
        {
            beatEdge = false;
            OnBeatEnd();
        } 
        else if (!beatEdge && beatDistance <= beatRolloffPeriod)
        {
            beatEdge = true;
            OnBeatStart();
        }

        foreach (MusicPlayer player in players)
        {
            player.Step(playingTime);
        }
    }

    private float GetActiveBeatTime()
    {
        float startOffset = playDelayTime + beatOffsetTime;

        if (playingTime < startOffset)
        {
            return startOffset;
        }
        else
        {
            float beatShift = (startOffset) % secondsPerBeat;//the number of seconds that the beat pattern is shifted by
            float closestTime = Mathf.Round(playingTime / secondsPerBeat) * secondsPerBeat + beatShift;//round to the closest time
            return closestTime;
        }
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

    [SerializeField]
    private float playingTime;//the amount of time that the song has been playing for
    private float playDelayTime;
    private float beatOffsetTime;
    private float secondsPerBeat;

    [SerializeField]
    private float beatRolloff = 0.1f;//the percent of the beat period to consider something on beat

    private float beatRolloffPeriod;

    private bool beatEdge;//if true then we are in an active beat
}
