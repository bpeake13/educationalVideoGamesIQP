using UnityEngine;
using System.Collections;

public class EditorRow : MonoBehaviour
{
    public int BeatIndex
    {
        get { return beatNumber; }
        set { beatNumber = value; }
    }

    public bool IsEmpty
    {
        get
        {
            bool bIsEmpty = true;
            for (int i = 0; i < 4; i++)
            {
                NoteEditorSlot slot = slots[i];
                if (slot.NoteType != null)
                {
                    bIsEmpty = false;
                    break;
                }
            }

            return bIsEmpty;
        }
    }

    /// <summary>
    /// Package back into row data
    /// </summary>
    /// <returns>The row data that this editor row represents</returns>
    public RowData Package()
    {
        bool bIsEmpty = true;
        for (int i = 0; i < 4; i++)
        {
            NoteEditorSlot slot = slots[i];
            if (slot.NoteType != null)
            {
                bIsEmpty = false;
                break;
            }
        }

        if (bIsEmpty)
            return null;

        NoteData[] newData = new NoteData[4];
        for (int i = 0; i < 4; i++)
        {
            Note noteType = slots[i].NoteType;
            if (!noteType)
                continue;

            NoteData note = new NoteData(noteType);
            newData[i] = note;
        }

        RowData row = new RowData(beatNumber);
        row.SetData(newData);

        return row;
    }

    public void Load(RowData data)
    {
        if (data == null)
            return;

        for(int i = 0; i < 4; i++)
        {
            slots[i].Load(data.GetNote(i));//load the note data into the row
        }

        beatNumber = data.BeatIndex;//copy over the beat index
    }

    public void Copy(EditorRow src)
    {
        for(int i = 0; i < 4; i++)
        {
            slots[i].NoteType = src.slots[i].NoteType;
        }
    }

    public void Clear()
    {
        for(int i = 0; i < 4; i++)
        {
            slots[i].Delete();
        }
    }

    [SerializeField]
    private NoteEditorSlot[] slots = new NoteEditorSlot[4];

    private int beatNumber;
}
