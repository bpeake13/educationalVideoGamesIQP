using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private string type;

    [SerializeField]
	private int health = 5;

    [SerializeField]
    private int maxHealth = 5;

    [SerializeField]
    private int damage = 5;

    [SerializeField]
    private int damageNumber = 1;

    private SpawnPoint spawnPoint;

    public int Health
    {
        get { return health; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int DamageNumber
    {
        get { return damageNumber; }
    }

    public SpawnPoint StartPoint
    {
        get { return spawnPoint; }
        set { spawnPoint = value; }
    }

	// Use this for initialization
	void Start () {
		type = "Unknown";
	}

	public void setType(string input) {
		type = input;
	}

	public string getType() {
		return type;
	}

    public virtual void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            onKilled();
        }
    }

    protected virtual void onKilled()
    {
        spawnPoint.Free();
    }
}
