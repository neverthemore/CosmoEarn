using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<BaseEnemy> enemies;

    [SerializeField] GameObject simpleEnemy;
    [SerializeField] Transform formationPoint;
    [SerializeField] int enemyCount = 10;

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    public void SpawnEnemy()
    {
        float minX = -8f;
        float maxX = 8f;

        float spawnY = -Camera.main.orthographicSize - 2f;

        Vector2 spawnPosition = new Vector2 (Random.Range(minX, maxX), spawnY);
        GameObject newEnemyObj = Instantiate(simpleEnemy, spawnPosition, Quaternion.identity);
        newEnemyObj.transform.SetParent(formationPoint, false);

        BaseEnemy newEnemy = newEnemyObj.GetComponent<BaseEnemy>();
        if (newEnemy != null )
        {
            enemies.Add(newEnemy);
        }
    }

    private IEnumerator StartSpawning()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(2);
        }
    }
}
