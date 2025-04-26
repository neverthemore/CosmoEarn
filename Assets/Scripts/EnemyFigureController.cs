using JetBrains.Annotations;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;


public class EnemyFigureController : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private Transform formationCenter;

    [SerializeField] float amplitude = 2f;
    [SerializeField] float frequency = 1f;

    [SerializeField] private float _spacing = 1.5f;

    [SerializeField] private float minRegroupTime = 7f;
    [SerializeField] private float maxRegroupTime = 15f;

    private float _currentRegroupTime = 5f;
    private float _currentRegroupCooldown = 0f;

    public enum FormationType { Triangle, Diamond, Raws, Circle}
    public FormationType _currentFormation = FormationType.Triangle;

    private Dictionary<FormationType, IFormationPattern> formationPatterns;

    private float elapsedTime;

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

        MoveCentralPoint();

        _currentRegroupCooldown += Time.deltaTime;
        if (_currentRegroupCooldown > _currentRegroupTime)
        {
            ChangeFormation();
            _currentRegroupTime = Random.Range(minRegroupTime, maxRegroupTime);
            _currentRegroupCooldown = 0f;
        }
        
    }

    void ChangeFormation()
    {
        int randomIndex = Random.Range(0, 3);
        if (randomIndex == 0) { _currentFormation = FormationType.Triangle; }
        else if (randomIndex == 2) { _currentFormation = FormationType.Circle; }
        else {  _currentFormation = FormationType.Raws;}
    }
    
    void MoveCentralPoint()
    {
        elapsedTime += Time.deltaTime;
        float offsetX = Mathf.Sin(elapsedTime * frequency) * amplitude;
        formationCenter.position = new Vector2(offsetX, formationCenter.position.y);
        //Влево-вправо по синусоиде

    }       
}
