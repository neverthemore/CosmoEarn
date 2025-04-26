using UnityEngine;
using System.Collections;

public class BossSpawn: ISpawnStrategy
{

    public IEnumerator ExecuteSpawn(EnemySpawner spawner)
    {
            spawner.SpawnBoss();
            yield return null;
    }
}
