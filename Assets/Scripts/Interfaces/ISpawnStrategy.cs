using System.Collections;
using UnityEngine;

public interface ISpawnStrategy
{
    IEnumerator ExecuteSpawn(EnemySpawner spawner);
}
