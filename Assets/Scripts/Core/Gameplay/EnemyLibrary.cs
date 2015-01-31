using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyLibrary : MonoBehaviour
{
    public static EnemyLibrary Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }

        foreach(Enemy e in enemies)
        {
            enemyTable.Add(e.name, e);
        }
    }

    public Enemy CreateEnemy(string name)
    {
        Enemy type = null;
        enemyTable.TryGetValue(name, out type);
        if (!type)
            return null;

        Enemy newEnemy = Instantiate(type) as Enemy;
        newEnemy.name = name;

        return newEnemy;
    }

    public Enemy CreateRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemies.Length);
        return CreateEnemy(enemies[randomIndex].name);
    }

    private Dictionary<string, Enemy> enemyTable = new Dictionary<string, Enemy>();

    [SerializeField]
    private Enemy[] enemies;

    private static EnemyLibrary instance;
}
