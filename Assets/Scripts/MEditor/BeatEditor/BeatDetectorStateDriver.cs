using UnityEngine;
using System.Collections;

public class BeatDetectorStateDriver : MonoBehaviour
{
    public enum EBeatDetectorState
    {
        WaitingForSamples,
        WaitingForGoodData,
        Fixing,
        Done
    }

    public void SetState(EBeatDetectorState newState)
    {
        if (currentState == newState)
            return;

        EBeatDetectorState oldState = currentState;
        currentState = newState;

        switch (currentState)
        {
            case EBeatDetectorState.WaitingForSamples:
                OnWaitingForSamples(oldState);
                break;
            case EBeatDetectorState.WaitingForGoodData:
                OnWaitingForGoodData(oldState);
                break;
            case EBeatDetectorState.Fixing:
                OnFixing(oldState);
                break;
            case EBeatDetectorState.Done:
                OnDone(oldState);
                break;
        }
    }

    public void SetStateDone()
    {
        SetState(EBeatDetectorState.Done);
    }

    public void SetStateWaitingForSamples()
    {
        SetState(EBeatDetectorState.WaitingForSamples);
    }

    private void OnWaitingForSamples(EBeatDetectorState oldState)
    {
        bpmText.Hide();
        indicator.SetBad();
        detector.gameObject.SetActive(true);
        driver.gameObject.SetActive(false);
        mainCanvasAnimator.SetTrigger("triggerBadSample");
    }

    private void OnWaitingForGoodData(EBeatDetectorState oldState)
    {
        bpmText.Show();
    }

    private void OnFixing(EBeatDetectorState oldState)
    {
        detector.gameObject.SetActive(false);
        driver.SetBPM(detector.CalculatedBPM);
        indicator.SetGood();

        mainCanvasAnimator.SetTrigger("triggerAskAlright");
    }

    private void OnDone(EBeatDetectorState oldState)
    {
    }

    private EBeatDetectorState currentState = EBeatDetectorState.WaitingForSamples;

    [SerializeField]
    private BPMText bpmText;

    [SerializeField]
    private BeatIndicator indicator;

    [SerializeField]
    private BeatDetector detector;

    [SerializeField]
    private BeatDriver driver;

    [SerializeField]
    private Animator mainCanvasAnimator;
}
