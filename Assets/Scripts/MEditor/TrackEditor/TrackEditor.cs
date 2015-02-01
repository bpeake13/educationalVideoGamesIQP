using UnityEngine;
using System.Collections;

public class TrackEditor : MonoBehaviour 
{
    public float DistancePerSecond
    {
        get { return distancePerSecond; }
    }

    /// <summary>
    /// Loads a song into the track editor.
    /// </summary>
    /// <param name="song">The song data to load</param>
    public void Load(Song song)
    {
        startPoint = trackStartTransform.position;
        endPoint = trackEndScrollPoint.position;
        this.song = song;
        StartCoroutine(LoadTrack(song));
    }

    public void Scroll(float percent)
    {
        float startToEndWidth = endPoint.x - startPoint.x;//get the width between the scroll bounds

        float actualScrollWidth = scrollWidth - startToEndWidth;//how far can the start transform actually go from the start point

        float offset = Mathf.Lerp(0, -actualScrollWidth, percent);//get the offset of the transform on the x-axis

        Vector3 position = startPoint + new Vector3(offset, 0, 0);
        trackStartTransform.position = position;
    }

    public void Save()
    {
        Track track = song.GetTrack(0);//get the first track
        int totalBeats = rows.Length;//get the total number of beats in a song, we can not have a percentage of beat so floor it to an int

        track.ClearData();

        for(int i = 0; i < totalBeats; i++)
        {
            RowData row = rows[i].Package();
            track.AddRow(row);
        }
    }

    public void Clear()
    {
        int totalBeats = rows.Length;

        for(int i = 0; i < totalBeats; i++)
        {
            rows[i].Clear();
        }
    }

    public void Generate()
    {
        int totalBeats = rows.Length;

        int lastRow = -1;
        for(int i = 0; i < totalBeats; i++)
        {
            EditorRow row = rows[i];
            if (!row.IsEmpty)
                lastRow = i;
        }

        for(int i = lastRow + 1; i < totalBeats; i++)
        {
            int srcIndex = i % (lastRow + 1);
            rows[i].Copy(rows[srcIndex]);
        }
    }

    private IEnumerator LoadTrack(Song song)
    {
        float bpm = song.BPM;//the bpm of the song
        AudioClip clip = song.Clip;//The audio clip that the song uses
        Track track = song.GetTrack(0);//get the first track

        float totalMinutes = clip.length / 60f;//get the number of minutes this song lasts for

        float secondsPerBeat = song.BeatPeriod;
        float offsetPerBeat = distancePerSecond * secondsPerBeat;

        float offset = 0;

        int totalBeats = Mathf.FloorToInt(bpm * totalMinutes);//get the total number of beats in a song, we can not have a percentage of beat so floor it to an int

        rows = new EditorRow[totalBeats];

        for (int i = 0; i < totalBeats; i++)//spawn every beat object
        {
            EditorRow row = Instantiate(editorRowTemplate) as EditorRow;//create a new editor row
            row.BeatIndex = i;
            row.Load(track.GetRow(i));
            rows[i] = row;

            Transform rowTransform = row.transform;

            rowTransform.position = trackStartTransform.position + new Vector3(offset, 0, 0);
            rowTransform.parent = trackStartTransform;

            offset += offsetPerBeat;//calculate the x offset for the next beat

            if (i % 100 == 0)
                yield return new WaitForEndOfFrame();
        }

        scrollWidth = offset - offsetPerBeat;
    }

    [SerializeField]
    [Tooltip("The transform to consider the start point for beats")]
    private Transform trackStartTransform;

    [SerializeField]
    [Tooltip("The transform to consider where the track should stop scrolling")]
    private Transform trackEndScrollPoint;

    [SerializeField]
    [Tooltip("The distance per each second of music for the notes")]
    private float distancePerSecond = 5;

    [SerializeField]
    [Tooltip("The prefab to use as the row editor")]
    private EditorRow editorRowTemplate;

    private EditorRow[] rows;

    private float scrollWidth;
    private Vector3 startPoint, endPoint;

    private Song song;
}
