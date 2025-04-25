using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;


public class EnemyFigureController : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private Transform formationCenter;

    [SerializeField] private float _spacing = 1.5f;

    public enum FormationType { Triangle, Diamond, Raws, Circle}
    public FormationType _currentFormation = FormationType.Triangle;

    private Dictionary<FormationType, IFormationPattern> formationPatterns;

    private void Awake()
    {
        formationPatterns = new Dictionary<FormationType, IFormationPattern>
        {
            {FormationType.Triangle, new TriangleFormation() },
            {FormationType.Circle, new CircleFormation() },
            {FormationType.Raws, new LineFormation() }
            
        };
    }
    void Update()
    {
        IFormationPattern pattern = formationPatterns[_currentFormation];
        List<Vector2> targetPositions = pattern.GetPoints(spawner.enemies.Count, formationCenter, _spacing);

        for (int i = 0; i < spawner.enemies.Count; i++)
        {
            spawner.enemies[i].SetTargetPosition(targetPositions[i]);
        }
    }
    
    void MoveCentralPoint()
    {
        //_centralPoint = transform.position;
    }       
}
