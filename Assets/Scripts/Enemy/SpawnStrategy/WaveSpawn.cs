using System.Collections;
using UnityEngine;

public class WaveSpawn : ISpawnStrategy
{
    private int enemiesPerWave;
    private float intervalBetweenWaves;
    private int totalWaves;

    public WaveSpawn(int enemiesPerWave, float intervalBetweenWaves, int totalWaves)
    {
        this.enemiesPerWave = enemiesPerWave;
        this.intervalBetweenWaves = intervalBetweenWaves;
        this.totalWaves = totalWaves;
    }

    public IEnumerator ExecuteSpawn(EnemySpawner spawner)
    {
        for (int wave = 0; wave < totalWaves; wave++)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                spawner.SpawnEnemy();
            }
        }

        yield return new WaitForSeconds(intervalBetweenWaves);
    }
}
