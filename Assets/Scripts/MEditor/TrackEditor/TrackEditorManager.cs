using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// Class in charge of managing the track editor
/// </summary>
public class TrackEditorManager : MonoBehaviour
{
    /// <summary>
    /// Get the static instance of the track editor manager.
    /// </summary>
    public static TrackEditorManager Instance
    {
        get { return instance; }
    }

    void Start()
    {
        Song defaultSong = SongLoader.Instance.GetSong();

        Load(defaultSong);
    }

    /// <summary>
    /// Loads a song into the editor.
    /// </summary>
    /// <param name="song">The song to load into the editor for editing.</param>
    public void Load(Song song)
    {
        if (song == null)
            return;

        this.song = song;

        track.Load(song);
        waveDisplay.Load(song);
    }

    /// <summary>
    /// Gets the current note type that is selected.
    /// </summary>
    /// <returns>The note type that is selected, or null if no note type is selected</returns>
    public Note GetSelectedNoteType()
    {
        Toggle activeToggle = null;
        foreach(Toggle toggle in noteGroup.ActiveToggles())
        {
            activeToggle = toggle;
        }

        if (!activeToggle)//if no toggle has been activated then return
            return null;

        NoteTypeButton noteButton = activeToggle.GetComponent<NoteTypeButton>();//get the note type button

        if(!noteButton)//if no note button has been created then log an error and return null
        {
            Debug.LogError("Note button created without a note type attached to it!");
            return null;
        }

        return noteButton.NoteType;//return the note type
    }

    public void SetTime(float time)
    {
        bIgnoreCancelShift = true;

        float totalTime = song.Clip.length;
        float percent = time / totalTime;
        percent = Mathf.Clamp01(percent);

        scroll.normalizedValue = percent;

        bIgnoreCancelShift = false;
    }

    public float GetTime()
    {
        float totalTime = song.Clip.length;
        float percent = scroll.value;

        return percent * totalTime;
    }

    /// <summary>
    /// Shifts the track over by a certain amount
    /// </summary>
    /// <param name="amount">The percent of a single row too shift the track over</param>
    public void Shift(float amount)
    {
        float time = GetTime();
        float bps = song.BPM / 60f;
        float spb = 1f / bps;

        float distance = spb * amount;
        targetTime = time + distance;

        bMoveToTargetTime = true;
    }

    public void CancelShift()
    {
        if (bIgnoreCancelShift)
            return;

        bMoveToTargetTime = false;
        scrollSpeed = 0f;
    }

    public void Play()
    {
        track.Save();

        Application.LoadLevel("SongLevel");
    }

    public void Save()
    {
        track.Save();//tell the track we are saving

        string fileName = song.SongFileName;
        string directoryName = song.SongDir;
        if (!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);

        FileStream stream;
        if (!File.Exists(fileName))
            stream = File.Create(fileName);
        else
            stream = new FileStream(fileName, FileMode.OpenOrCreate);

        BinaryWriter writer = new BinaryWriter(stream);
        song.Serialize(writer);

        writer.Close();
    }

    public void OpenOptions()
    {
        Application.LoadLevel("TrackEditorOptions");
    }

    public void Generate()
    {
        track.Generate();
    }

    public void Close()
    {
        Save();

        Application.LoadLevel("EditorMainMenu");
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(bMoveToTargetTime)
        {
            float time = GetTime();
            float newTime = Mathf.SmoothDamp(time, targetTime, ref scrollSpeed, desiredScrollTime);
            SetTime(newTime);

            if(Mathf.Abs(time - targetTime) <= 0.001f)
            {
                SetTime(targetTime);
                CancelShift();
            }
        }
    }

    private static TrackEditorManager instance;//static instance of this manager


    [SerializeField]
    [Tooltip("The toggle group that will contain the different note types that can be selected and placed.")]
    private UnityEngine.UI.ToggleGroup noteGroup;

    [SerializeField]
    private WaveformDisplay waveDisplay;

    [SerializeField]
    private UnityEngine.UI.Slider scroll;

    [SerializeField]
    private TrackEditor track;

    private Song song;

    private float targetTime;
    private bool bMoveToTargetTime;
    private float desiredScrollTime = 0.5f;
    private float scrollSpeed = 0f;

    private bool bIgnoreCancelShift;
}
