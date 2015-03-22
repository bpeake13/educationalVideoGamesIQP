using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Counter))]
public class Player : MonoBehaviour 
{
	private GameObject io;
	private Student_IO IOScript = new Student_IO();

	private GameObject sf;
	private Student_Functions SFunctions = new Student_Functions();

	// The currently loaded student data
	Student_Data studentData;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;

	public AudioSource hurt;
	private float timer = 0f;

    public static Player Instance
    {
        get { return instance; }
    }

    void Start()
    {
        instance = this;
        healthCounter = GetComponent<Counter>();
        healthCounter.OnCounterChanged.AddListener(OnHealthChanged);

		io = GameObject.Find("Student_IO");
		IOScript = (Student_IO) io.GetComponent(typeof(Student_IO));
		sf = GameObject.Find("SFunctions");
		SFunctions = (Student_Functions) sf.GetComponent(typeof(Student_Functions));
		// Application.dataPath.Remove(Application.dataPath.Length - 7) + @"/Student Data/";
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		studentData = spscript.getStudentData();
    }

	void Update() {
		// This should probably have its own script
		if(timer > 0f) {
			timer -= Time.deltaTime;
		}
		if(timer <= 0f) {
			Camera.main.backgroundColor = new Color(49f/255f, 77f/255f, 121/255f);
		}
	}

    public void Hurt(int damage)
    {
        healthCounter.subtract(damage);
		//hurt.Play ();
		Camera.main.backgroundColor = new Color(66f/255f, 77f/277f, 121/277f);;
		timer = 0.2f;
    }

    void OnHealthChanged(int value)
    {
        if(value <= 0)
        {
            OnKilled();
        }
    }

    protected virtual void OnKilled()
    {
		string filepath = Application.dataPath + @"/Student Data/";
		// Update student data
		SFunctions.UpdateStudentMetrics ();
		// Export the data
		IOScript.Export(filepath, studentData);
        Application.LoadLevel("GameOver");
    }

    private Counter healthCounter;

    private static Player instance;
}
