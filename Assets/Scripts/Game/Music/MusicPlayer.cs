using UnityEngine;
using System.Collections;

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
    }

    public void OnBeatEnd()
    {
    }

    [SerializeField]
    private int playerIndex;

    private MusicDriver driver;
    private Track track;
}
