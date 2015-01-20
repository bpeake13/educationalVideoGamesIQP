using UnityEngine;
using System.Collections;

public class MonsterEditorScript : MonoBehaviour
{
    void Start()
    {
        Song song = SongLoader.Instance.GetSong();


    }

    public void Save()
    {

    }

    public void AddMonster()
    {
        string[] monsterTypes = new string[0];

        //selectorDialogueCreator.CreateDialogue(monsterTypes, OnMonsterCreated, "Select a monster type!", "There are no monster types.");
        OnMonsterCreated(null);
    }

    private void OnMonsterCreated(string result)
    {
        //if (string.IsNullOrEmpty(result))
           // return;

        RectTransform monsterElement = Instantiate(monsterElementTemplate) as RectTransform;

        monsterElement.SetParent(monsterElementTemplate.parent, false);
        monsterElement.SetSiblingIndex(monsterElement.GetSiblingIndex() - 1);
        monsterElement.gameObject.SetActive(true);
    }

    [SerializeField]
    private SelectorDialogueCreator selectorDialogueCreator;

    [SerializeField]
    private RectTransform monsterElementTemplate;
}
