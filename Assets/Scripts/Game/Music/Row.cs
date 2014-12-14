using UnityEngine;
using System.Collections;

public class Row : MonoBehaviour
{

	public void Setup(MusicDriver driver)
	{
		this.driver = driver;
	}

	public int BeatIndex
	{
		get{return beatIndex;}
		set{beatIndex = value;}
	}

	public bool Valid
	{
		get{return isValid;}
		set{isValid = value;}
	}

    /// <summary>
    /// Sets the row to the specified data
    /// </summary>
    /// <param name="data">The data to set the row to.</param>
    /// <para name="beatLocation">A transform with the location of where the bar should be on beat</param>
    public void SetData(RowData data, Transform beatLocation, float timeTill)
    {
		if(data == null)
			return;

        for (int i = 0; i < 4; i++)//generate notes from the data
        {
            Note note = data.GetNote(i).CreateNote();

            note.transform.position = notePoints[i].position;
            note.transform.parent = transform;
        }

		beatIndex = data.BeatIndex;//copy the beat index

		// Probably don't need this anymore
        Vector3 offset = transform.position - beatLocation.position;
        velocity = offset * (1f / timeTill);
    }

    public void DeleteData()
    {
        for (int i = 0; i < 4; i++)
        {
            Note note = notes[i];

            if(note)
                Destroy(note.gameObject);
        }
    }

	void Start() {
		// Get the music driver
	    driver = (MusicDriver)GameObject.Find("MusicDriver").GetComponent(typeof(TestMusicDriver));
	}

    void Update()
    {
		int onScreenBars = 8;
		float bpm = driver.CurrentSong.BPM;
		float startX = 35f;
		float endX = -14f;

		lifetimer += Time.deltaTime;

		if(bpm > 0) {
			transform.position = new Vector3(((((60f/(float)bpm) - lifetimer/onScreenBars)/(60f/(float)bpm))*startX + endX), transform.position.y, transform.position.z);
		}

		// Experimentally removed
        //Vector3 delta = velocity * Time.deltaTime;

        //transform.position += delta;

		// Destroy object if too far to the right
		if(transform.position.x < -22f) {
			Destroy (gameObject);
			lifetimer = 0f;
		}
    }

	public MusicDriver driver;

    [SerializeField]
    private Transform[] notePoints = new Transform[4];

    private Vector3 velocity;

	private float lifetimer = 0f;

	private int beatIndex;

    private Note[] notes = new Note[4];

	private bool isValid = false;
	
	private Song song;
}
