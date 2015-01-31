using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Counter))]
public class EnemyManager : MonoBehaviour {

	private Counter accumulater; // The amount of stored attack
	private bool isActive = true; // Whether the mob will be targeted in attacks
	private AudioSource sword;

	Student_Data studentData;
	private GameObject sp; // The student profile object
	private Student_Profile spscript;
	
	private GameObject gm; // The game metric object
	private Game_Metric gmscript;

	private List<Enemy> currentEnemies = new List<Enemy>();

    [SerializeField]
    private int maxNumberOfEnemies = 3;

    [SerializeField]
    private SpawnPoint[] spawnPoints;

    [SerializeField]
    private EnemyLibrary enemyLib;

	// NOTE: All monster types are handled from this one class right now, consider using sub classes

	private static EnemyManager monsters;

	public static EnemyManager Instance
	{
		get {
			if (monsters) {
				return monsters;
			}
			
			GameObject obj = new GameObject("Monster", typeof(EnemyManager));
			monsters = obj.GetComponent<EnemyManager>();
			monsters.Initialization();
			
			return monsters;
		}
	}
	
	// Use this for initialization
	void Initialization () {
        accumulater = GetComponent<Counter>();
        accumulater.OnCounterChanged.AddListener(onAccumulaterValueChanged);
		// Init sword sound effect
		sword = gameObject.AddComponent<AudioSource>();
		sword.clip = Resources.Load("sword_clash") as AudioClip;
		// Student data script
		sp = GameObject.Find("Profile");
		spscript = (Student_Profile) sp.GetComponent(typeof(Student_Profile));
		studentData = spscript.getStudentData();
		// Game Metric script
		gm = GameObject.Find("GameMetric");
		gmscript = (Game_Metric) gm.GetComponent(typeof(Game_Metric));
	}

	// Update is called once per frame
	void Update ()
    {
        foreach(SpawnPoint sp in spawnPoints)
        {
            if (!sp.IsTaken)
                sp.Spawn();
        }
	}

	// Will add to the accumulater
	public void addAccumulater(int value)
    {
        accumulater.add(value);
    }

    public void subtractAccumulater(int value)
    {
        accumulater.subtract(value);
    }

    public void multiplyAccumulater(int value)
    {
        accumulater.multiply(value);
    }

    public void divideAccumulater(int value)
    {
        accumulater.divide(value);
    }

	public void releaseAccumulater() {
        accumulater.Value = 0;
		// Then will destroy all enemies with the same value as the accumulater
	}

	public int getAccumulaterValue() {
        return accumulater.Value;
	}

	public string getEnemyType(int index) {
		return currentEnemies[index].getType();
	}

	public Enemy getEnemy(int index)
    {
        return currentEnemies[index];
    }

	public List<Enemy> getAllCurrentEnemies() {
		return currentEnemies;
	}

    private void onAccumulaterValueChanged(int newValue)
    {
        bool wasDamageDelt = false;
        foreach(Enemy e in currentEnemies)
        {
            if(e.DamageNumber == newValue)
            {
                e.takeDamage(newValue);
                wasDamageDelt = true;
            }
        }

        if (wasDamageDelt)
            accumulater.Value = 0;
    }
}
