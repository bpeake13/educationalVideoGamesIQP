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
    public void SetData(RowData data)
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

    [SerializeField]
    private Transform[] notePoints = new Transform[4];

	private int beatIndex;

    private Note[] notes = new Note[4];

	private bool isValid = false;
}
