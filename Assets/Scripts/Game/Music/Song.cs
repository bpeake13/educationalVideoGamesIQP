using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class Song
{
	static bool populate = false;
	void Update() {
		// Add a default track to the song
		if(!populate) {
			Track t = new Track();
			tracks.Add (t);
			populate = true;
		}
	}

    public string Name
    {
        get { return name; }
    }

    /// <summary>
    /// The Beats Per Minute of the song
    /// </summary>
    /// <value>A float that represents the BPM</value>
    public float BPM
    {
        get{ return bpm;}
        set { bpm = value; }
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

            DirectoryInfo dir = new DirectoryInfo(SongDir);
            if (!dir.Exists)
            {
                Debug.LogError("Song directory does not exist.");
                return null;
            }
			string loaderPath;
			if (Application.platform == RuntimePlatform.WindowsPlayer ||
			    Application.platform == RuntimePlatform.WindowsEditor) {
           		loaderPath = "file://" + dir.FullName + @"\\" + audioFile;
			} else if (Application.platform == RuntimePlatform.OSXPlayer ||
			    Application.platform == RuntimePlatform.OSXEditor) {
				loaderPath = "file://" + dir.FullName + @"/" + audioFile;
			} else {
				loaderPath = "file://" + dir.FullName + @"\\" + audioFile;
				Debug.Log ("OS not identified as mac or windows, an error may ensue.");
			}
            WWW loader = new WWW(loaderPath);

            while (!loader.isDone) { }

            clip = loader.GetAudioClip(false, false, type);
    
            return clip;
        }
    }

    public string AudioFileName
    {
        get { return audioFile; }
        set { audioFile = value; }
    }

    public string SongDir
    {
        get
        {
			return Path.Combine(@"Songs", Name);
        }
    }

    public string SongFileName
    {
        get
        {
            return Path.Combine(SongDir, "info.song");
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

    public Song(string name)
    {
        this.name = name;
        tracks.Add(new Track());
    }

    private Song()
    { }

    public static Song Deserialize(BinaryReader reader)
    {
        Song s = new Song ();

        //read header data
        s.name = reader.ReadString();
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

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(name);
        writer.Write(bpm);
        writer.Write(startDelay);
        writer.Write(timeOffset);
        writer.Write(audioFile);

        writer.Write(tracks.Count);

        foreach (Track t in tracks)
        {
            t.Serialize(writer);
        }
    }

    /// <summary>
    /// Gets the audio type that this song should be loaded as
    /// </summary>
    /// <returns>The audio type to load as</returns>
    public AudioType GetAudioType()
    {
        FileInfo info = new FileInfo (audioFile);

        string extension = info.Extension.ToUpper ().Substring (1);
        switch(extension)
        {
            case "WAV":
                return AudioType.WAV;
            case "OGG":
                return AudioType.OGGVORBIS;
            case "MP2":
                goto case "MP3";
            case "MP3":
                return AudioType.MPEG;
        }

        return AudioType.UNKNOWN;
    }

    /// <summary>
    /// Converts a time in the song to a beat number
    /// </summary>
    /// <returns>The to beat number, a beat number of zero means before the song has started.</returns>
    /// <param name="t">The time to convert.</param>
    public int TimeToBeat(float t)
    {
        float onBeatDelta = BeatPeriod * beatTimeAmount;
        float timePassed = t - (startDelay + timeOffset);
        if (timePassed + onBeatDelta < 0)
            return 0;

        if(clip != null)
            timePassed = Mathf.Min(timePassed, clip.length);

        float bps = bpm / 60f;//get the number of beats in a second
        int beats = Mathf.RoundToInt(bps * timePassed) + 1;

        return beats;
    }

    /// <summary>
    /// Gets the closest beat time.
    /// </summary>
    /// <returns>The closest beat time.</returns>
    /// <param name="time">The real time we are at in the song.</param>
    /// <param name="onBeat">When on beat this is set to true, otherwise false.</param>
    public float GetClosestBeatTime(float time, out bool onBeat)
    {
        float secondsPerBeat = BeatPeriod;
        float startOffset = startDelay + timeOffset;
        
        float beatTime;
        
        if (time < startOffset)
        {
            beatTime = startOffset;
        }
        else
        {
            float beatShift = (startOffset) % secondsPerBeat;//the number of seconds that the beat pattern is shifted by
            float closestTime = Mathf.Round(time / secondsPerBeat) * secondsPerBeat + beatShift;//round to the closest time
            beatTime = closestTime;
        }
        
        float onBeatDelta = secondsPerBeat * beatTimeAmount;
        onBeat = Mathf.Abs(time - beatTime) <= onBeatDelta;
        return beatTime;
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
    private string name = "Untitled";

    [SerializeField]
    //[Tooltip("The beats per minute of the song")]
    private float bpm = 120;

    [SerializeField]
    //[Tooltip("The percent of the beat period that is considered to be on beat")]
    private float beatTimeAmount = 0.05f;

    [SerializeField]
    //[Tooltip("The path to the audio file to use")]
    private string audioFile;

    [SerializeField]
    //[Tooltip("The amount of time (in seconds) that we will wait before considering it the first beat")]
    private float timeOffset;

    [SerializeField]
    //[Tooltip("The amount of time to wait before starting the song when we play")]
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
            if(row.BeatIndex < 0)
                continue;

            track.beats.Add(row);
            track.beatTable.Add(row.BeatIndex, row);
        }

        return track;
    }

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(beats.Count);

        foreach (RowData r in beats)
        {
            r.Serialize(writer);
        }
    }

    public void ClearData()
    {
        beats.Clear();
        beatTable.Clear();
    }

    public void AddRow(RowData row)
    {
        if (row == null)
            return;

        int index = row.BeatIndex;

        RemoveRow(index);//remove the row to overwrite

        int beatsCount = beats.Count;

        for(int i = 0; i < beatsCount; i++)
        {
            RowData other = beats[i];

            if(index > other.BeatIndex)
            {
                beats.Insert(i, row);
                beatTable.Add(index, row);
                return;
            }
        }

        beats.Add(row);
        beatTable.Add(index, row);
    }

    public void RemoveRow(int beatIndex)
    {
        if(beatTable.ContainsKey(beatIndex))
        {
            RowData rowData = beatTable[beatIndex];
            beats.Remove(rowData);
            beatTable.Remove(beatIndex);
        }
    }

    /// <summary>
    /// Gets the row at the beat
    /// </summary>
    /// <returns>The row data at the beat.</returns>
    /// <param name="beat">The beat index.</param>
    public RowData GetRow(int beat)
    {
        if (!beatTable.ContainsKey(beat))
            return null;
        return beatTable [beat];
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
                note = NoteData.Deserialize(reader);

            rd.notes[i] = note;
        }

        return rd;
	}

	public RowData() {
		// Default population, replace later
		for (int i = 0; i < 4; i++)
		{
			NoteData note = new NoteData();
			notes[i] = note;
		}
	}

    public RowData(int beatIndex)
    {
        for (int i = 0; i < 4; i++)
        {
            NoteData note = new NoteData();
            notes[i] = note;
        }

        this.beat = beatIndex;
    }

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(beat);

        for(int i = 0; i < 4; i++)
        {
            NoteData note = notes[i];
            writer.Write(note != null);

            if(note != null)
                notes[i].Serialize(writer);
        }
    }

    public NoteData GetNote(int index)
    {
        return notes [index];
    }

    public void SetData(NoteData[] noteData)
    {
        Array.Copy(noteData, notes, 4);
    }

    [SerializeField]
    private int beat; //the beat number in the song we are at

	[SerializeField]
	private NoteData[] notes = new NoteData[4];
}

/// <summary>
/// Contains information about a single note in a song
/// </summary>
[Serializable]
public class NoteData
{
    public NoteData()
    {

    }

    public NoteData(Note source)
    {
        if (source)
            noteType = source.name;
        else
            noteType = "";
    }

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

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(noteType);
    }

    /// <summary>
    /// Creates a note randomly
    /// </summary>
    /// <returns>A note instance that was created, or null on failiure.</returns>
    [Obsolete("This is a testing method and will be replaced by CreateNote in release versions", false)]
	public Note CreateNoteRandom()
	{
		// commented this out because it broke things
        //if (string.IsNullOrEmpty(noteType))
        //    return null;

        NoteTypeLib lib = NoteTypeLib.Instance;
		Note note;
		if(UnityEngine.Random.Range(0, 2) == 0) {
        	note = lib.getNoteType("Note 1"); // TODO: Temporary value
		} else
		if(UnityEngine.Random.Range(0, 2) == 1) {
			note = lib.getNoteType("Note 2"); // TODO: Temporary value
		} else {
			note = lib.getNoteType("Note 3"); // TODO: Temporary value
		}

		return GameObject.Instantiate(note, new Vector3( 20f, 0.836f, 4.89f), Quaternion.Euler(270, 270, 270)) as Note;
	}

    public Note CreateNote()
    {
        if (string.IsNullOrEmpty(noteType))
            return null;

        NoteTypeLib lib = NoteTypeLib.Instance;
        Note note = lib.getNoteType(noteType);

		Note newNote = GameObject.Instantiate(note, new Vector3( 20f, 0.836f, 4.89f), Quaternion.Euler(270, 270, 270)) as Note;
        newNote.name = noteType;

        return newNote;
    }

	[SerializeField]
	private string noteType;
}