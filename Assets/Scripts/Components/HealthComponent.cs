using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;


public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 3;
    private int currentHealth;

    [SerializeField] bool _isFriendly = false;

    
    [SerializeField] Animator[] animator;

    [SerializeField] GameObject energyCore;

    [SerializeField] private bool _isPlayer = false;
    [SerializeField] UpgradeData upgrade;

    private void Awake()
    {
        if (_isPlayer)
        {
            maxHealth = (int)upgrade.GetCurrentValue();
            currentHealth = maxHealth;
            Debug.Log("Жизней: " + currentHealth);
        }

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
        if (!_isPlayer)
        {
            Instantiate(energyCore, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            //Выход в меню
        }
           
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
