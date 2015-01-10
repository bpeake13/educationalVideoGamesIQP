using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoteTypeView : MonoBehaviour
{
    void Start()
    {
        NoteTypeLib lib = NoteTypeLib.Instance;

        if(!lib)//log an error if the note lib is not found
        {
            Debug.LogError("Note lib not found!");
            return;
        }

        Note[] noteTypes = lib.getNoteTypes();

        foreach(Note noteType in noteTypes)
        {
            NoteTypeButton newButton = Instantiate(templateButton) as NoteTypeButton;
            newButton.transform.SetParent(templateButton.transform.parent, false);

            newButton.SetNoteType(noteType);
        }

        Destroy(templateButton.gameObject);

        GetComponent<ToggleGroup>().SetAllTogglesOff();
    }

    [SerializeField]
    private NoteTypeButton templateButton;

    [SerializeField]
    private Vector2 buttonSpacing = new Vector2(5, 5);
}
