using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Boss : BaseEnemy
{
    [System.Serializable]
    public class BossStage
    {
        public string stageName;
        public System.Action<Boss> stageAI;
        public System.Action<Boss> stageAttack;
        public System.Action<Boss> stageInit;
        public System.Action<Boss> stageFinish;
        public string[] nextStages;
        public float timeLimit = -1f;
        public float healthThreshold = -1f; // 0-1 процент здоровья
    }

    [Header("Boss Settings")]
    [SerializeField] private BossStage[] bossStages;
    [SerializeField] private float stageTransitionDelay = 1f;
    [SerializeField] private int bossScore = 1000;
    [SerializeField] private float itemDropRate = 0.002f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private float deathEffectSize = 2f;
    [SerializeField] private float deathEffectSpeed = 4f;

    private Dictionary<string, BossStage> stages = new Dictionary<string, BossStage>();
    private string currentStage;
    private string nextStage;
    private float stageTimer;
    private float healthThresholdTimer;

    private List<BaseGun> bossGuns = new List<BaseGun>();

    protected override void Awake()
    {
        base.Awake();
        InitializeStages();
        FindBossGuns();
    }

    private void InitializeStages()
    {
        foreach (var stage in bossStages)
        {
            stages.Add(stage.stageName, stage);
        }
    }

    private void FindBossGuns()
    {
        bossGuns.AddRange(GetComponentsInChildren<BaseGun>());
    }

    protected override void Update()
    {
        base.Update();

        if (!IsPaused)
        {
            UpdateStageTimers();
            ExecuteStageAI();
            TryFireAllGuns();
        }
    }

    private void UpdateStageTimers()
    {
        if (stageTimer > 0)
        {
            stageTimer -= Time.deltaTime;
            if (stageTimer <= 0) SwitchStage();
        }

        if (healthThresholdTimer > 0)
        {
            healthThresholdTimer -= Time.deltaTime;
        }
    }

    private void ExecuteStageAI()
    {
        if (currentStage != null && stages.ContainsKey(currentStage))
        {
            stages[currentStage].stageAI?.Invoke(this);
        }
    }

    private void TryFireAllGuns()
    {
        foreach (var gun in bossGuns)
        {
            gun.Attack();
        }
    }

    public override void SetTargetPosition(Vector2 targetPos)
    {
        base.SetTargetPosition(targetPos);

        // Дополнительная логика для босса при изменении позиции
        if (currentStage != null && stages.ContainsKey(currentStage))
        {
            // Можно добавить stage-специфичные реакции
        }
    }

    public override void Fire()
    {
        if (currentStage != null && stages.ContainsKey(currentStage))
        {
            stages[currentStage].stageAttack?.Invoke(this);
        }
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);

        if (currentStage != null && stages.ContainsKey(currentStage))
        {
            var stage = stages[currentStage];

            // Проверка перехода по здоровью
            if (stage.healthThreshold > 0 && healthThresholdTimer <= 0)
            {
                float healthPercent = CurrentHealth / MaxHealth;
                if (healthPercent <= stage.healthThreshold)
                {
                    healthThresholdTimer = stageTransitionDelay;
                    SetNextStage();
                }
            }
        }
    }

    private void SetNextStage()
    {
        if (currentStage != null && stages.ContainsKey(currentStage))
        {
            var current = stages[currentStage];

            if (current.nextStages != null && current.nextStages.Length > 0)
            {
                nextStage = current.nextStages[Random.Range(0, current.nextStages.Length)];
            }
        }
    }

    private void SwitchStage()
    {
        // Завершаем текущую стадию
        if (currentStage != null && stages.ContainsKey(currentStage))
        {
            stages[currentStage].stageFinish?.Invoke(this);
        }

        // Начинаем новую стадию
        if (nextStage != null && stages.ContainsKey(nextStage))
        {
            currentStage = nextStage;
            var newStage = stages[currentStage];

            newStage.stageInit?.Invoke(this);
            stageTimer = newStage.timeLimit > 0 ? newStage.timeLimit : -1f;

            // Устанавливаем следующий возможный переход
            SetNextStage();
        }
        else
        {
            currentStage = null;
        }
    }

    protected override void OnDeath()
    {
        // Очистка пуль
        BulletManager.Instance.ClearAllEnemyBullets();

        // Награда за убийство
        ScoreManager.Instance.AddScore(bossScore);

        // Эффект смерти
        PlayDeathEffect();

        // Дроп предметов
        TryDropItems();

        // Переход на следующий уровень
        LevelManager.Instance.LoadNextLevel();

        base.OnDeath();
    }

    private void PlayDeathEffect()
    {
        if (deathEffect != null)
        {
            var effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            var main = effect.main;
            main.startSize = deathEffectSize;
            main.startSpeed = deathEffectSpeed;
        }
    }

    private void TryDropItems()
    {
        if (Random.value < itemDropRate)
        {
            ItemManager.Instance.SpawnRandomItem(transform.position);
        }
    }
}