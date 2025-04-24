using UnityEngine;


public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 3;
    private int currentHealth;

    [SerializeField] bool _isFriendly = false;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("������� ����");
        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        //� ���� ����
        //�������� �������
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth += amount, maxHealth);
    }

    public bool IsFriendly()
    {
        return _isFriendly;
    }
}
