using UnityEngine;

public class BossHealthComponent : HealthComponent
{
    [SerializeField] GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }


    protected override void Die()
    {
        panel.SetActive(true);
        base.Die();

    }
}
