using UnityEngine;
using System.Collections;

public class BeatIndicator : MonoBehaviour
{
    void Awake()
    {
        anim = GetComponent<Animator>();
        onBeatHitHash = Animator.StringToHash("BeatHit");
        triggerGood = Animator.StringToHash("TriggerGood");
        triggerBad = Animator.StringToHash("TriggerBad");
    }

    public void BeatHit()
    {
        anim.SetTrigger(onBeatHitHash);
    }

    public void SetGood()
    {
        anim.SetTrigger(triggerGood);
    }

    public void SetBad()
    {
        anim.SetTrigger(triggerBad);
    }

    private int onBeatHitHash;
    private int triggerGood;
    private int triggerBad;

    private Animator anim;
}
