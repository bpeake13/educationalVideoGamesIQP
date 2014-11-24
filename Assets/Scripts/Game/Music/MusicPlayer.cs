using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour
{
	public void Setup(MusicDriver driver)
    {
        this.driver = driver;
        this.track = driver.CurrentSong.GetTrack(playerIndex);
    }

    public void Step(float time)
    {

    }

    public void OnBeatStart()
    {
		rows.Peek().Valid = true;
    }

    public void OnBeatEnd()
    {
		rows.Dequeue().Valid = false;
    }

    [SerializeField]
    private int playerIndex;

	[SerializeField]
	[Tooltip("The prefab to use for spawning rows of notes.")]
	private Row rowPrefab;

    private MusicDriver driver;
    private Track track;

	private Queue<Row> rows = new Queue<Row>();//the list of rows that are currently scrolling
}
