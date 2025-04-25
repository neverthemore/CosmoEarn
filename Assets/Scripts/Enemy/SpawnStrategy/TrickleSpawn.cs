using UnityEngine;
using System.Collections;

public class TrickleSpawn : ISpawnStrategy
{
    private int totalEnemies;
    private float interval;

    public TrickleSpawn(int totalEnemies, float interval)
    {
        this.totalEnemies = totalEnemies;
        this.interval = interval;
    }

    public IEnumerator ExecuteSpawn(EnemySpawner spawner)
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            spawner.SpawnEnemy();
            yield return new WaitForSeconds(interval);
        }
    }
}
