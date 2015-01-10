using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class SelectorDialogue : MonoBehaviour
{
    public delegate void OnDialogueClosedEvent(string selectedItem);

    public static void Show(string[] items, OnDialogueClosedEvent onClosedEvent, string title, string errorMessage = "")
    {
        SelectorDialogueCreator creator = FindObjectOfType<SelectorDialogueCreator>();
        if(!creator)
        {
            GameObject obj = new GameObject("SelectorCreator", typeof(SelectorDialogueCreator));
            creator = obj.GetComponent<SelectorDialogueCreator>();
        }

        creator.CreateDialogue(items, onClosedEvent, title, errorMessage);
    }

    void Start()
    {
        Canvas[] uis = FindObjectsOfType<Canvas>();

        int depth = int.MinValue;
        foreach(Canvas ui in uis)
        {
            if (ui.sortingOrder > depth)
                depth = ui.sortingOrder;
        }

        GetComponent<Canvas>().sortingOrder = depth + 1;
    }

    public void Settup(string[] items, OnDialogueClosedEvent onClosedEvent, string title, string errorMessage = "")
    {
        this.onClosedEvent = onClosedEvent;

        this.title.text = title;
        if (items.Length == 0)
            this.errorMessage.text = errorMessage;
        else
            this.errorMessage.gameObject.SetActive(false);

        bool isFirst = true;
        foreach(string item in items)
        {
            GameObject newButton = Instantiate(buttonTemplate) as GameObject;

            newButton.transform.SetParent(buttonTemplate.transform.parent, false);
            Text text = newButton.GetComponentInChildren<Text>();
            text.text = item;

            if(isFirst)
            {
                isFirst = false;

                newButton.GetComponent<Toggle>().isOn = true;
            }
        }

        Destroy(buttonTemplate);
    }

    public void Ok()
    {
        Close();

        foreach(Toggle t in toggleGroup.ActiveToggles())
        {
            string text = t.GetComponentInChildren<Text>().text;
            onClosedEvent(text);
            return;
        }

        onClosedEvent(null);
    }
    
    public void Cancel()
    {
        Close();

        onClosedEvent(null);
    }

    private void Close()
    {
        Destroy(gameObject);
    }

    [SerializeField]
    private GameObject buttonTemplate;

    [SerializeField]
    private ToggleGroup toggleGroup;

    [SerializeField]
    private Text title;

    [SerializeField]
    private Text errorMessage;

    private OnDialogueClosedEvent onClosedEvent;
}
