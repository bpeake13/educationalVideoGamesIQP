using UnityEngine;
using System.Collections;

/// <summary>
/// A slot to have a note on
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class NoteEditorSlot : MonoBehaviour
{
    public Note NoteType
    {
        get { return note; }
        set
        {
            if (note)//destroy the pre-existing note if it exists
            {
                Destroy(note.gameObject);
                note = null;
            }

            if (!value)
                return;

            note = Instantiate(value) as Note;//create a new copy of the note
            note.name = note.name.Replace("(Clone)", "");
            note.transform.parent = this.transform;
            note.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            note.transform.localEulerAngles = new Vector3(-90, 180, 0);
            note.transform.localPosition = Vector3.zero;
        }
    }

    public void Load(NoteData data)
    {
        if (data == null)
            return;

        note = data.CreateNote();
        note.transform.parent = this.transform;
        note.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        note.transform.localEulerAngles = new Vector3(-90, 180, 0);
        note.transform.localPosition = Vector3.zero;
    }

    public void Delete()
    {
        if (note)//right click to delete the note
        {
            Destroy(note.gameObject);
            note = null;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))//get left button
            bLeftClicked = true;

        if (Input.GetMouseButtonDown(1))//get right button
            bRightClicked = true;

        if (Input.GetMouseButtonDown(2))
            bMiddleClicked = true;

        if(bLeftClicked && Input.GetMouseButtonUp(0))
        {
            bLeftClicked = false;
            OnLeftMouse();
        }

        if (bRightClicked && Input.GetMouseButtonUp(1))
        {
            bRightClicked = false;
            OnRightMouse();
        }

        if (bMiddleClicked && Input.GetMouseButtonUp(2))
        {
            bMiddleClicked = false;
            OnMiddleMouse();
        }
    }

    void OnMouseExit()
    {
        bLeftClicked = false;
        bRightClicked = false;
        bMiddleClicked = false;
    }

    protected virtual void OnLeftMouse()
    {
        TrackEditorManager editor = TrackEditorManager.Instance;//get the editor instance

        Note noteTemplate = editor.GetSelectedNoteType();//get the selected template note
        if (!noteTemplate)//when no note template is selected do nothing
            return;

        NoteType = noteTemplate;
    }

    protected virtual void OnRightMouse()
    {
        Delete();
    }

    protected virtual void OnMiddleMouse()
    {

    }

    private bool bLeftClicked = false;
    private bool bRightClicked = false;
    private bool bMiddleClicked = false;

    private Note note;
}
