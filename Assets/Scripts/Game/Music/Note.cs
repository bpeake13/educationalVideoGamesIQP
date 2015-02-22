using UnityEngine;
using System.Collections;

public abstract class Note : MonoBehaviour
{
    public abstract void Execute();

    public virtual void OnMiss() { }
}
