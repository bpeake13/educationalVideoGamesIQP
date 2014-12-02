using UnityEngine;
using System.Collections;

public class Row : MonoBehaviour
{
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

    void Update()
    {
        Vector3 delta = velocity * Time.deltaTime;

        transform.position += delta;
    }

    [SerializeField]
    private Transform[] notePoints = new Transform[4];

    private Vector3 velocity;

	private int beatIndex;

    private Note[] notes = new Note[4];

	private bool isValid = false;
}
