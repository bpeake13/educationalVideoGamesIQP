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
        float deltaTime = time - lastTime;

        float spawnTime = time + 2f;//get 2 seconds ahead to spawn a beat

        bool wasBeat = false;

        float beatTime = driver.CurrentSong.GetClosestBeatTime(spawnTime, out wasBeat);

        if (wasBeat)
        {
            int beatIndex = driver.CurrentSong.TimeToBeat(beatTime);

            if(beatIndex > beatNumber)//spawn a row if we have a new beat
            {
                Row r = Instantiate(rowPrefab, rowSpawnPoint.position, Quaternion.identity) as Row;
                r.SetData(track.GetRow(beatIndex), beatLocation, 2f);

                beatNumber = beatIndex;

                rows.Enqueue(r);
            }
        }

        lastTime = time;
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
	//[Tooltip("The prefab to use for spawning rows of notes.")]
	private Row rowPrefab;

    [SerializeField]
    private Transform rowSpawnPoint;

    [SerializeField]
    private Transform beatLocation;

    private MusicDriver driver;
    private Track track;

    private float lastTime;

    private int beatNumber = 0;

	private Queue<Row> rows = new Queue<Row>();//the list of rows that are currently scrolling
}
