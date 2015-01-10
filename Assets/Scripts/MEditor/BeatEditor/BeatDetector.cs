using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BeatDetector : MonoBehaviour
{
    public float CalculatedBPM
    {
        get{ return calculatedBPM;}
    }

    void Start()
    {
        source = FindObjectOfType<AudioSource>();//get the audio source in the scene
    }

	// Update is called once per frame
	void Update ()
	{
        float delta = Time.deltaTime;//get the time passed
        
        totalTimer += delta;//increase the timer by the delta time

        if (bTimerStarted)
        {
            timer += delta;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                beatDeltas.AddLast(timer);
                beatTimes.AddLast(source.time);//get the time on the song

                timer = 0;

                CalculateBPM();

                indicator.BeatHit();
            }
        } else
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                bTimerStarted = true;
                indicator.BeatHit();
                timer = 0;
            }
        }
	}

    private bool CalculateBPM()
    {
        int deltaCount = beatDeltas.Count;
        if (deltaCount < 10)//not enough data collection to get a good calculation
            return false;

        float sum = 0;
        LinkedListNode<float> deltaNode = beatDeltas.First;

        for (; deltaNode != null; deltaNode = deltaNode.Next)
        {
            float d = deltaNode.Value;
            sum += d;//get the sum
        }

        float avg = sum / deltaCount;

        float sqrSum = 0;
        deltaNode = beatDeltas.First;

        for (; deltaNode != null; deltaNode = deltaNode.Next)
        {
            float d = deltaNode.Value - avg;
            float v = d * d;
            sqrSum += v;
        }

        float standardDeviation = Mathf.Sqrt(sqrSum / deltaCount);//get the standard deviation

        if (standardDeviation > 0.1f)//do not let the delta time deviate more than a tenth of a second
        {
            if(deltaCount >= 15)//data to dirty, clear data
            {
                beatDeltas.Clear();
                beatTimes.Clear();

                bTimerStarted = false;

                stateDriver.SetState(BeatDetectorStateDriver.EBeatDetectorState.WaitingForSamples);
            }
            return false;
        }

        float bps = 1f / avg;
        calculatedBPM = bps * 60f;

        bpmText.text = string.Format("{0:00}\nBPM", calculatedBPM);

        if (deltaCount >= 30)
        {
            stateDriver.SetState(BeatDetectorStateDriver.EBeatDetectorState.Fixing);
            return true;
        }

        stateDriver.SetState(BeatDetectorStateDriver.EBeatDetectorState.WaitingForGoodData);

        return true;
    }

	private float timer;

	private float totalTimer;

	private bool bTimerStarted;

	private LinkedList<float> beatDeltas = new LinkedList<float>();//list of delta times a button was pressed

	private LinkedList<float> beatTimes = new LinkedList<float>();//list of times the button was pressed

    private float calculatedBPM;

    private bool bIsGoodValue;

    private AudioSource source;

    [SerializeField]
    private BeatDetectorStateDriver stateDriver;

    [SerializeField]
    [Tooltip("The acceptable level of deviation we are allowed.")]
    private float acceptableDeviation;

    [SerializeField]
    private BeatIndicator indicator;

    [SerializeField]
    private Text bpmText;
}
