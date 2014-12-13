using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteTypeLib : MonoBehaviour
{
    public static NoteTypeLib Instance
    {
        get{ return instance;}
    }

    /// <summary>
    /// Gets the note of the specified type.
    /// </summary>
    /// <returns>The note of the specified type, or null if the type was not found</returns>
    /// <param name="name">The name of the note type to find.</param>
    public Note getNoteType(string name)
    {
        Note note = null;
        noteTable.TryGetValue(name, out note);
        return note;
    }

    public Note[] getNoteTypes()
    {
        return noteTypes.ToArray();
    }

    void Awake()
    {
        int noteTypeCount = noteTypes.Count;
        for (int i = 0; i < noteTypeCount; i++)//add all note types to the table
        {
            Note n = noteTypes[i];
            noteTable.Add(n.name, n);
        }

        instance = this;
    }

    [SerializeField]
    //[Tooltip("A listing of different types of notes that can be used")]
    private List<Note> noteTypes = new List<Note>();

    private Dictionary<string, Note> noteTable = new Dictionary<string,Note>();//table used at runtime to lookup note types

    private static NoteTypeLib instance;//the static instance of the note lib
}
