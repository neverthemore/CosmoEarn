using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;


public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 3;
    private int currentHealth;

    [SerializeField] bool _isFriendly = false;

    [SerializeField] Animator[] animator;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //animator[Random.Range(0, animator.Length)].Play();
        Debug.Log("Получен урон: " + name);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);    
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
