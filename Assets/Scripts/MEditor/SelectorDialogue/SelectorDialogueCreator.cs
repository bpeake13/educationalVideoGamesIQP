using UnityEngine;
using System.Collections;

public class SelectorDialogueCreator : MonoBehaviour
{
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void CreateDialogue(string[] items, SelectorDialogue.OnDialogueClosedEvent onClosed, string title, string errorMessage)
    {
        SelectorDialogue newDialogue = Instantiate(template) as SelectorDialogue;
        newDialogue.Settup(items, onClosed, title, errorMessage);
    }

    [SerializeField]
    private SelectorDialogue template;
}
