using UnityEngine;
using System.Collections;

public class Track_Spawner : MonoBehaviour {

	public GameObject track;
	private MusicDriver driver;
	private static float timer = 0f;

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
		if(timer > 60f/driver.CurrentSong.BPM) {
			// Spawn point (20f, 0.836f, 4.89f)
			GameObject o = (GameObject)Instantiate (track, new Vector3( 100f, 0.836f, 4.89f), Quaternion.Euler(270, 0, 0));
			//script = (Row)driver.GetComponent(typeof(Row));
			//script.Setup(driver);
			timer -= 60f/driver.CurrentSong.BPM;
		}
	}
}
