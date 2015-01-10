using UnityEngine;
using System.Collections;

public class Track_Spawner : MonoBehaviour {

	public GameObject track;
	private MusicDriver driver;
	private Row script;
	private float timer = 0f;
	private float lifetime = 0f;

	// Use this for initialization
	void Start () {
		//for(int i = 0; i < 10; i++) {
			//GameObject tp = (GameObject)Instantiate (track, new Vector3(i * 2.0F - 7.47f, 0.836f, 4.89f), Quaternion.Euler(270, 0, 0));
		//}
		driver = (MusicDriver)GameObject.Find("MusicDriver").GetComponent(typeof(TestMusicDriver));
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		lifetime += Time.deltaTime;
		if(timer > 60f/driver.CurrentSong.BPM && lifetime < driver.CurrentSong.Clip.length) {
			// Spawn point (20f, 0.836f, 4.89f)
			GameObject o = (GameObject)Instantiate (track, new Vector3( 100f, 0.836f, 4.89f), Quaternion.Euler(270, 0, 0));
			timer -= 60f/driver.CurrentSong.BPM;
			script = (Row)o.GetComponent(typeof(Row));
			script.SetData(new RowData());
		}
	}
}
