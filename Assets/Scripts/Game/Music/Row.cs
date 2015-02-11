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
    public void SetData(RowData data)
    {
		if(data == null)
			return;

        for (int i = 0; i < 4; i++)//generate notes from the data
        {
			if(data.GetNote(3 - i) != null) {
	            Note note = data.GetNote(3 - i).CreateNote();

				// Note must not be null
				if(note != null) {
                    note.transform.localScale = note.transform.localScale * 1.8f;
                    note.transform.parent = notePoints[i].transform;
                    note.transform.localPosition = Vector3.zero;
                    note.transform.localRotation = Quaternion.identity;
					notes[i] = note;
				}
			}
        }

		beatIndex = data.BeatIndex;//copy the beat index

		// Probably don't need this anymore
        //Vector3 offset = transform.position - beatLocation.position;
        //velocity = offset * (1f / timeTill);
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
		// Game Metric script
		gm = GameObject.Find("GameMetric");
		gmscript = (Game_Metric) gm.GetComponent(typeof(Game_Metric));
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

	// Executes a note's action and handles scoring and sound effects
	// returns false if input registers a miss, else true
	private bool executeNote(int index) {
		if(notes[index] == null) {
			// Whoops, missed no note here
			//gmscript.misses += 1;
			//gmscript.score -= 10;
			return false;
		}
		pSystems[index].Play();
		Destroy (notes[index].gameObject);
		// Hit
		gmscript.hits += 1;
		gmscript.score += 10;
		isHit = true;
		notes[index].Execute();
		goodTone.Play ();
		return true;
	}

	// returns false if input registers a miss
	public bool ExecuteInput() {
		// User input
		if(!isHit && (transform.position.x > -17f && transform.position.x < -13f)) {
			if(Input.GetKeyDown ("a")) {
				return executeNote (3);
			} else
			if(Input.GetKeyDown ("s")) {
				return executeNote (2);
			} else
			if(Input.GetKeyDown ("d")) {
				return executeNote (1);
			} else
			if(Input.GetKeyDown ("f")) {
				return executeNote (0);
			}
		}
		//If can no longer hit, deduct score ir row has notes
		if(!isHit && transform.position.x < -16f) {
			// You cannot miss a row with no notes in it
			if(notes[0] == null && notes[1] == null && notes[2] == null && notes[3] == null) {
				return true;
			} else {
				// Whoops, missed the beat
				// Score deduction removed
				//gmscript.misses += 1;
				//gmscript.score -= 10;
			}
			isHit = true;
		}
		if(Input.GetKeyDown ("a") || Input.GetKeyDown ("s") || Input.GetKeyDown ("d") || Input.GetKeyDown ("f")) {
			return false;
		}
		return true;
	}

	public MusicDriver driver;

	public AudioSource goodTone;

	private GameObject gm; // The student profile object
	private Game_Metric gmscript;

    [SerializeField]
    private Transform[] notePoints = new Transform[4];

	[SerializeField]
	private ParticleSystem[] pSystems = new ParticleSystem[4];

    private Vector3 velocity;

	private float lifetimer = 0f;

	private int beatIndex;

    private Note[] notes = new Note[4];

	private bool isValid = false;

	private bool isHit = false;
	
	private Song song;
}
