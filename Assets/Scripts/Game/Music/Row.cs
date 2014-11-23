using UnityEngine;
using System.Collections;

public class Row : MonoBehaviour
{
    /// <summary>
    /// Sets the row to the specified data
    /// </summary>
    /// <param name="data">The data to set the row to.</param>
    public void SetData(RowData data)
    {
        for (int i = 0; i < 4; i++)
        {
            Note note = data.GetNote(i).CreateNote();

            note.transform.position = notePoints[i].position;
            note.transform.parent = transform;
        }
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

    private Note[] notes = new Note[4];
}
