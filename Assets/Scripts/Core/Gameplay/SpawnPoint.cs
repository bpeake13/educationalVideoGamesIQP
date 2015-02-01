using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    public bool IsTaken
    {
        get { return enemySlot != null; }
    }

    public Enemy Spawn(string enemyType)
    {
        if (enemySlot != null)
            return null;

        Enemy newEnemy = EnemyLibrary.Instance.CreateEnemy(enemyType);
        newEnemy.transform.position = transform.position;
        enemySlot = newEnemy;
        newEnemy.StartPoint = this;

        return newEnemy;
    }

    public Enemy Spawn()
    {
        if (enemySlot != null)
            return null;

        Enemy newEnemy = EnemyLibrary.Instance.CreateRandomEnemy();
        newEnemy.transform.position = transform.position;
        enemySlot = newEnemy;
        newEnemy.StartPoint = this;

        return newEnemy;
    }

    public void Free()
    {
        enemySlot = null;
    }

    private Enemy enemySlot;//the enemy that is occupying this space
}
