using JetBrains.Annotations;
using System;
using System.Drawing;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class EnemyFigureController : MonoBehaviour
{
    [SerializeField] BaseEnemy[] enemies;
    private int _maxNumOfEnemy;
    private Vector2 _centralPoint;
    void Start()
    {
        _maxNumOfEnemy = enemies.Length;
        _centralPoint = transform.position;
    }

    void moveCentralPoint()
    {
        _centralPoint = transform.position;
    }
    void MakePiramid(Vector2 spawn, int n, float delta)
    {
        _centralPoint = spawn;
        int enemyIndex = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < i+1; j++)
            {
                float xOffset = (j - i / 2f) * delta;
                float yOffset = -i * delta; 
                enemies[enemyIndex].transform.localPosition = new Vector2(_centralPoint.x + xOffset, _centralPoint.y + yOffset);
                enemyIndex++;
            }
        }
    }    

    void MakeSquare(Vector2 spawn, int n, float radius)
    {
        int sideLen = (int)Mathf.Sqrt(n);
        _centralPoint = spawn;
        int enemyIndex = 0;
        for (int i = 0; i < sideLen; i++)
        {
            for (int j = 0; j < sideLen; j++)
            {                
                float xOffset = -radius + j * (radius * 2) / (sideLen - 1);
                float yOffset = -radius + i * (radius * 2) / (sideLen - 1);
                enemies[enemyIndex].transform.localPosition = new Vector2(_centralPoint.x + xOffset, _centralPoint.y + yOffset);
                enemyIndex++;
            }
        }
    }
    void DestroyEnemy()
    {
        
    }

    void ChangingFigures()
    {
        
    }
    
    void Update()
    {
        MakePiramid(new Vector2(0, 0.5f), 5, 1);
    }
}
