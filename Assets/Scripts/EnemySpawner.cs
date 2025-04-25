using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<BaseEnemy> enemies;

    [SerializeField] GameObject simpleEnemy;
    [SerializeField] Transform formationPoint;

    private ISpawnStrategy currentStrategy;

    private void Start()
    {
        var trickle = new TrickleSpawn(10, 1.5f);
        SetStrategy(trickle);

        //Можно сделать Scriptable для изменений спавна врагов, и постепенно запускать через события или через время
        //+ я бы сделал так, что формирования делились например по 10 кораблей на фигуру
    }

    public void SetStrategy(ISpawnStrategy strategy)
    {
        currentStrategy = strategy;
        StartCoroutine(currentStrategy.ExecuteSpawn(this));
    }

    public void SpawnEnemy()
    {
        float minX = -8f;
        float maxX = 8f;

        float spawnY = Camera.main.orthographicSize + 2f;

        Vector2 spawnPosition = new Vector2 (Random.Range(minX, maxX), spawnY);
        GameObject newEnemyObj = Instantiate(simpleEnemy, spawnPosition, Quaternion.identity);
        //newEnemyObj.transform.SetParent(formationPoint, false);

        BaseEnemy newEnemy = newEnemyObj.GetComponent<BaseEnemy>();
        if (newEnemy != null )
        {
            enemies.Add(newEnemy);
        }
    }

}
