using UnityEngine;
using System.Collections;

public class DefendNote : Note
{
    public override void Execute()
    {
        Enemy[] enemies = EnemyManager.Instance.getAllCurrentEnemies();

        int enemyCount = enemies.Length;
        if (enemyCount == 0)
            return;

        Enemy randEnemy = enemies[Random.Range(0, enemyCount)];

        randEnemy.Attack(false);
    }

    public override void OnMiss()
    {
        Enemy[] enemies = EnemyManager.Instance.getAllCurrentEnemies();

        int enemyCount = enemies.Length;
        if (enemyCount == 0)
            return;

        Enemy randEnemy = enemies[Random.Range(0, enemyCount)];

        randEnemy.Attack(true);
    }
}
