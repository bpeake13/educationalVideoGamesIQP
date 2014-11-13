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

	public static Song Deserialize(BinaryReader reader)
	{
		Song s = new Song ();

        //read header data
		s.bpm = reader.ReadSingle ();
		s.startDelay = reader.ReadSingle ();
		s.timeOffset = reader.ReadSingle ();
		s.audioFile = reader.ReadString ();

        //read in the rows and add them to the beats list
		int rowCount = reader.ReadInt32 ();
		for (int i = 0; i < rowCount; i++)
        {
            RowData rd = RowData.Deserialize(reader);
            s.beats.Add(rd);
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
	private List<RowData> beats = new List<RowData> ();

	private AudioClip clip;
}

/// <summary>
/// A single row of notes
/// </summary>
[Serializable]
public class RowData
{
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

	public UnityEngine.Object CreateNote()
	{
		GameObject go = new GameObject ();

		return go;
	}

	[SerializeField]
	private string noteType;
}