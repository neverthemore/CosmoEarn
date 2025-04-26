using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<BaseEnemy> enemies;

    [SerializeField] GameObject simpleEnemy;
    [SerializeField] GameObject bossEnemy;
    [SerializeField] Transform formationPoint;

    private int currentLevel = 0;

    private float checkSpawn = 0f;

    private float checkCooldown = 1f;
    private float currentCooldown = 0f;

    private ISpawnStrategy currentStrategy;

    private void Start()
    {
        var trickle = new TrickleSpawn(10, 1.5f);
        SetStrategy(trickle);

        //Можно сделать Scriptable для изменений спавна врагов, и постепенно запускать через события или через время
        //+ я бы сделал так, что формирования делились например по 10 кораблей на фигуру
    }

    private void Update()
    {
        currentCooldown += Time.deltaTime;

        if (currentCooldown >= checkCooldown)               //Плохая реализация
        {
            enemies.RemoveAll(enemy => enemy == null);
            currentCooldown = 0f;
        }

        if (enemies.Count == 0)
        {
            checkSpawn += Time.deltaTime;
        }

        if (checkSpawn > 5f)
        {
            currentLevel++;
            if (currentLevel == 10) SpawnBoss();
            SetStrategy(ChooseStartegy());
            checkSpawn = 0f;
        }
    }

    public void SetStrategy(ISpawnStrategy strategy)
    {
        currentStrategy = strategy;
        StartCoroutine(currentStrategy.ExecuteSpawn(this));
    }

    private ISpawnStrategy ChooseStartegy()
    {
        //Либо волна, либо случ. спавн
        if (Random.Range(0,2) == 0)
        {
            return new TrickleSpawn(currentLevel * 3, 1f);
        }
        else
        {
            return new WaveSpawn(currentLevel * 2, currentLevel * 2, currentLevel);
        }

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

    public void SpawnBoss()
    {
        GameObject boss = Instantiate(bossEnemy, new Vector2(0, -5.4f), Quaternion.identity);
    }

}
