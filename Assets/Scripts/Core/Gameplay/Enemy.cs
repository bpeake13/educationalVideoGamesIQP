using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
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

    [SerializeField]
    private string idleAnimation;

    [SerializeField]
    private string attackAnimation;

    [SerializeField]
    private string hurtAnimation;

    [SerializeField]
    private string killAnimation;

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
        animator = GetComponent<Animator>();
        animator.Play(idleAnimation);
		// Game Metric script
		gm = GameObject.Find("GameMetric");
		gmscript = (Game_Metric)gm.GetComponent(typeof(Game_Metric));
	}

	public void setType(string input) {
		type = input;
	}

	public string getType() {
		return type;
	}

    public virtual void takeDamage(int damage)
    {
		gmscript.score += 90;
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            onKilled();
        }
        else
            animator.Play(hurtAnimation);
    }

    protected virtual void onKilled()
    {
		gmscript.score += 100;
        animator.Play(killAnimation);
    }

    private void killFinished()
    {
        Destroy(gameObject);
        spawnPoint.Free();
    }

    private Animator animator;

	private GameObject gm; // The game metric object
	private Game_Metric gmscript;
}
