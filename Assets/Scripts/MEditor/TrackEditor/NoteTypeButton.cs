﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Toggle))]
public class NoteTypeButton : MonoBehaviour
{
    public Note NoteType
    {
        get { return note; }
    }

    public RectTransform GetRectTransform()
    {
        return GetComponent<RectTransform>();
    }

    public void SetNoteType(Note template)
    {
        Note newNote = Instantiate(template, transform.position - new Vector3(0, 0, 10), Quaternion.identity) as Note;//spawn a new note at our location and a little closer to the camera
        newNote.name = newNote.name.Replace("(Clone)", "");
        newNote.transform.SetParent(transform, true);
        note = template;
    }

    private Note note;
}
