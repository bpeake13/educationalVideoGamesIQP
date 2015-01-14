using UnityEngine;
using System.Collections;

public abstract class Achievement : MonoBehaviour {

	private bool unlocked = false;
	protected string description = "No Description";

	public bool getUnlockState() {
		if(checkUnlockCondition()) {
			unlocked = true;
		}
		return unlocked;
	}

	public string getDescription() {
		return description;
	}

	public virtual bool checkUnlockCondition() {
		return false;
	}
}
