using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    public bool IsTaken
    {
        get { return enemySlot != null; }
    }

    public void Spawn(string enemyType)
    {
        if (enemySlot != null)
            return;

        Enemy newEnemy = EnemyLibrary.Instance.CreateEnemy(enemyType);
        enemySlot = newEnemy;
        newEnemy.StartPoint = this;
    }

    public void Spawn()
    {
        if (enemySlot != null)
            return;

        Enemy newEnemy = EnemyLibrary.Instance.CreateRandomEnemy();
        enemySlot = newEnemy;
        newEnemy.StartPoint = this;
    }

    public void Free()
    {
        enemySlot = null;
    }

    private Enemy enemySlot;//the enemy that is occupying this space
}
