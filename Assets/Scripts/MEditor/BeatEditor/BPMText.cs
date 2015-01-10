using UnityEngine;
using System.Collections;

public class BPMText : MonoBehaviour {

	void Awake()
    {
        animator = GetComponent<Animator>();

        showHash = Animator.StringToHash("show");
        hideHash = Animator.StringToHash("hide");
    }

    public void Show()
    {
        animator.SetTrigger(showHash);
    }

    public void Hide()
    {
        animator.SetTrigger(hideHash);
    }
	
    private Animator animator;

    private int showHash, hideHash;
}
