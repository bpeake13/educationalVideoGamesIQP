using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Counter))]
public class Player : MonoBehaviour 
{
    public static Player Instance
    {
        get { return instance; }
    }

    void Start()
    {
        instance = this;
        healthCounter = GetComponent<Counter>();
        healthCounter.OnCounterChanged.AddListener(OnHealthChanged);
    }

    public void Hurt(int damage)
    {
        healthCounter.subtract(damage);
    }

    void OnHealthChanged(int value)
    {
        if(value <= 0)
        {
            OnKilled();
        }
    }

    protected virtual void OnKilled()
    {
        Application.LoadLevel("GameOver");
    }

    private Counter healthCounter;

    private static Player instance;
}
