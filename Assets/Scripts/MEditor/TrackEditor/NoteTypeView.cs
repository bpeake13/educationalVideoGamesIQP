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

        RectTransform buttonRect = templateButton.GetRectTransform();
        float buttonWidth = buttonRect.offsetMax.x - buttonRect.offsetMin.x;

        RectTransform containerRectTransform = GetComponent<RectTransform>();
        float newRectWidth = (buttonWidth + buttonSpacing.x) * noteTypes.Length;
        float currentRectWidth = containerRectTransform.rect.width;
        containerRectTransform.sizeDelta = new Vector2(newRectWidth - currentRectWidth, 0);

        float xPos = buttonWidth / 2f + buttonSpacing.x + containerRectTransform.rect.xMin;

        ToggleGroup group = GetComponent<ToggleGroup>();

        foreach(Note nt in noteTypes)
        {
            NoteTypeButton newNoteButton = Instantiate(templateButton) as NoteTypeButton;//spawn a new button
            newNoteButton.transform.position = new Vector3(xPos, 0, 0);
            newNoteButton.transform.SetParent(transform, false);

            newNoteButton.SetNoteType(nt);

            xPos += buttonWidth + buttonSpacing.x;

            newNoteButton.GetComponent<Toggle>().group = group;
        }

        group.SetAllTogglesOff();
    }

    [SerializeField]
    private NoteTypeButton templateButton;

    [SerializeField]
    private Vector2 buttonSpacing = new Vector2(5, 5);
}
