using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class Song
{
    /// <summary>
    /// The Beats Per Minute of the song
    /// </summary>
    /// <value>A float that represents the BPM</value>
    public float BPM
    {
        get{ return bpm;}
    }

    /// <summary>
    /// How long to wait before starting the song
    /// </summary>
    /// <value>The start time.</value>
    public float StartDelay
    {
        get{ return startDelay;}
    }

    /// <summary>
    /// The amount of time (in seconds) that we should offset the first beat by after the start delay
    /// </summary>
    /// <value>The beat offset.</value>
    public float BeatOffset
    {
        get{ return timeOffset;}
    }

    public AudioClip Clip
    {
        get
        {
            if(clip)
                return clip;

            AudioType type = GetAudioType();
            if(type == AudioType.UNKNOWN)
                return null;

            WWW loader = new WWW("file://" + audioFile);
            clip = loader.GetAudioClip(false, true, type);
    
            return clip;
        }
    }

    /// <summary>
    /// Gets the number of seconds that a beat is considered active
    /// </summary>
    /// <value>The time in seconds that a beat is active</value>
    public float BeatPeriod
    {
        get
        {
            float bps = bpm / 60f;
            return 1f / bps;
        }
    }

    public static Song Deserialize(BinaryReader reader)
    {
        Song s = new Song ();

        //read header data
        s.bpm = reader.ReadSingle ();
        s.startDelay = reader.ReadSingle ();
        s.timeOffset = reader.ReadSingle ();
        s.audioFile = reader.ReadString ();

        //read in the number of tracks
        int trackCount = reader.ReadInt32 ();
        for (int i = 0; i < trackCount; i++)
        {
            Track t = Track.Deserialize(reader);
            s.tracks.Add(t);
        }

        return s;//return the newly created song
    }

    /// <summary>
    /// Gets the audio type that this song should be loaded as
    /// </summary>
    /// <returns>The audio type to load as</returns>
    public AudioType GetAudioType()
    {
        FileInfo info = new FileInfo (audioFile);

        string extension = info.Extension.ToUpper ().Substring (1);

        AudioType parsedType = AudioType.UNKNOWN;
        try
        {
            parsedType = (AudioType)Enum.Parse(typeof(AudioType), extension);
        }
        catch(ArgumentException)
        {
            return AudioType.UNKNOWN;
        }

        return parsedType;
    }

    /// <summary>
    /// Converts a time in the song to a beat number
    /// </summary>
    /// <returns>The to beat number, a beat number of zero means before the song has started.</returns>
    /// <param name="t">The time to convert.</param>
    public int TimeToBeat(float t)
    {
        float timePassed = t - (startDelay + timeOffset);
        if (timePassed < 0)
            return 0;

        if(clip != null)
            timePassed = Mathf.Min(timePassed, clip.length);

        float bps = bpm / 60f;//get the number of beats in a second
        int beats = Mathf.FloorToInt(bps * timePassed) + 1;

        return beats;
    }

    /// <summary>
    /// Gets a track from the track list
    /// </summary>
    /// <returns>The track at the index</returns>
    /// <param name="index">The track index to get, this number will be clamped to the correct bounds</param>
    public Track GetTrack(int index)
    {
        index = Mathf.Clamp(index, 0, tracks.Count - 1);

        return tracks [index];
    }

    [SerializeField]
    [Tooltip("The beats per minute of the song")]
    private float bpm = 120;

    [SerializeField]
    [Tooltip("The path to the audio file to use")]
    private string audioFile;

    [SerializeField]
    [Tooltip("The amount of time (in seconds) that we will wait before considering it the first beat")]
    private float timeOffset;

    [SerializeField]
    [Tooltip("The amount of time to wait before starting the song when we play")]
    private float startDelay;

    [SerializeField]
    private List<Track> tracks = new List<Track>();//list of all tracks in this song

    [SerializeField]
    private AudioClip clip;
}

[Serializable]
public class Track
{
    public static Track Deserialize(BinaryReader reader)
    {
        Track track = new Track();

        int beatCount = reader.ReadInt32();
        for (int i = 0; i < beatCount; i++)
        {
            RowData row = RowData.Deserialize(reader);
            if(row.BeatIndex <= 0)
                continue;

            track.beats.Add(row);
            track.beatTable.Add(row.BeatIndex, row);
        }

        return track;
    }

	[SerializeField]
	private List<RowData> beats = new List<RowData> ();

    private Dictionary<int, RowData> beatTable = new Dictionary<int, RowData>();
}

/// <summary>
/// A single row of notes
/// </summary>
[Serializable]
public class RowData
{
    /// <summary>
    /// Gets the beat that this row happens on
    /// </summary>
    /// <value>The index of the beat.</value>
    public int BeatIndex
    {
        get{return beat;}
    }

    /// <summary>
    /// Deserializes a row from a binary stream
    /// </summary>
    /// <param name="reader">The binary reader to read from</param>
	public static RowData Deserialize(BinaryReader reader)
	{
        RowData rd = new RowData();

        rd.beat = reader.ReadInt32();

        for (int i = 0; i < 4; i++)
        {
            NoteData note;

            bool validNote = reader.ReadBoolean();
            if(!validNote)
                note = null;
            else
                note = null;

            rd.notes[i] = note;
        }

        return rd;
	}

    public NoteData GetNote(int index)
    {
        return notes [index];
    }

    [SerializeField]
    private int beat;//the beat number in the song we are at

	[SerializeField]
	private NoteData[] notes = new NoteData[4];
}

/// <summary>
/// Contains information about a single note in a song
/// </summary>
[Serializable]
public class NoteData
{
    /// <summary>
    /// Deserializes a note from a binary stream
    /// </summary>
    /// <param name="reader">The read to read the note data from</param>
    public static NoteData Deserialize(BinaryReader reader)
    {
        NoteData nd = new NoteData();

        nd.noteType = reader.ReadString();

        return nd;
    }

    /// <summary>
    /// Creates a note from the note data
    /// </summary>
    /// <returns>A note instance that was created, or null on failiure.</returns>
	public Note CreateNote()
	{
        NoteTypeLib lib = NoteTypeLib.Instance;
        Note note = lib.getNoteType(noteType);

        return GameObject.Instantiate(note) as Note;
	}

	[SerializeField]
	private string noteType;
}