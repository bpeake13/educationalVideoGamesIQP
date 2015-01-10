using UnityEngine;
using System.Collections;

public class BeatDriver : MonoBehaviour
{
    public void SetBPM(float bpm)
    {
        gameObject.SetActive(true);

        beatTime = 1f / (bpm / 60f);//get the amount of time between beats and set our timer to it
        timer = beatTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            float remainder = -timer;
            timer = beatTime - remainder;

            indicator.BeatHit();
        }
    }

    private float timer;

    private float beatTime;

    [SerializeField]
    private BeatIndicator indicator;
}
