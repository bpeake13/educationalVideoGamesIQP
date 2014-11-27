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
		source.loop = false;
		source.Stop();

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

	public void Stop()
	{
		source.Stop();
		isPlaying = false;
		isPaused = false;
	}

	public void Pause()
	{
		source.Pause();
		isPaused = true;
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	protected virtual void Update()
	{
		if(!isPlaying || isPaused)//do nothing if not playing or paused
			return;

		float lastPlayTime = playingTime;

		float startDelay = song.StartDelay;
		if(!source.isPlaying)//if our audio source is not playing
		{
			if(playingTime >= startDelay)//if we should be playing then set the play position to the current recorded play time and start playing
			{
				source.time = playingTime - startDelay;
				source.Play();
			}
			else//otherwise keep our timer going
			{
				playingTime += Time.deltaTime;
			}
		}
		else//if we are playing then our play time should be recorded from the audios to prevent the two from de-syncing
		{
			playingTime = source.time + startDelay;
		}

		float playTimeDelta = playingTime - lastPlayTime;//calculate a delta from the change in play times

		bool isBeat = false;
		float nearestBeatTime = song.GetClosestBeatTime(playingTime, out isBeat);

		if(isBeat && !wasOnBeat)//rising edge
		{
			OnBeatStart();
		}
		else if(!isBeat && wasOnBeat)//falling edge
		{
			OnBeatEnd();
		}

		wasOnBeat = isBeat;
	}

	/// <summary>
	/// Called on the start of a beat period
	/// </summary>
    protected virtual void OnBeatStart()
    {
        foreach (MusicPlayer player in players)
        {
            player.OnBeatStart();
        }
    }

	/// <summary>
	/// Called on the end of a beat period
	/// </summary>
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

	/// <summary>
	/// The current position in the song track
	/// </summary>
	private float playingTime;

	private bool wasOnBeat;

    private AudioSource source;
}
