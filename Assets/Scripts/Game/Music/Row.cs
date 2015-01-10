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
            Note note = data.GetNote(i).CreateNote();

            note.transform.position = notePoints[i].position;
            note.transform.parent = transform;
			notes[i] = note;
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

	// returns false if input registers a miss
	public bool ExecuteInput() {
		// User input
		if(!isHit && (transform.position.x > -16f && transform.position.x < -13f)) {
			if(Input.GetKeyDown ("a")) {
				pSystems[3].Play();
				Destroy (notes[3].gameObject);
				// Hit
				gmscript.hits += 1;
				gmscript.score += 10;
				isHit = true;
				notes[3].Execute();
				goodTone.Play ();
				return true;
			} else
			if(Input.GetKeyDown ("s")) {
				pSystems[2].Play();
				Destroy (notes[2].gameObject);
				// Hit
				gmscript.hits += 1;
				gmscript.score += 10;
				isHit = true;
				notes[2].Execute();
				goodTone.Play ();
				return true;
			} else
			if(Input.GetKeyDown ("d")) {
				pSystems[1].Play();
				Destroy (notes[1].gameObject);
				// Hit
				gmscript.hits += 1;
				gmscript.score += 10;
				isHit = true;
				notes[1].Execute();
				goodTone.Play ();
				return true;
			} else
			if(Input.GetKeyDown ("f")) {
				pSystems[0].Play();
				Destroy (notes[0].gameObject);
				// Hit
				gmscript.hits += 1;
				gmscript.score += 10;
				isHit = true;
				notes[0].Execute();
				goodTone.Play ();
				return true;
			}
		}
		//If can no longer hit, deduct score
		if(!isHit && transform.position.x < -16f) {
			// Whoops, missed the beat
			gmscript.misses += 1;
			gmscript.score -= 10;
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
