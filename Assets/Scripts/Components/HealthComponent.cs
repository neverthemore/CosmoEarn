using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 3;
    private int currentHealth;

    [SerializeField] bool _isFriendly = false;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] Animator[] animator;

    [SerializeField] GameObject energyCore;

    [SerializeField] private bool _isPlayer = false;
    [SerializeField] UpgradeData upgrade;

    private void Awake()
    {   
            _audioSource = GetComponent<AudioSource>();
            _audioSource = gameObject.AddComponent<AudioSource>();
        
        if (_isPlayer)
        {
            maxHealth = (int)upgrade.GetCurrentValue();
            Debug.Log("Жизней: " + currentHealth);
        }
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

    protected virtual void Die()
    {
        if (!_isPlayer)
        {        
            Instantiate(energyCore, transform.position, Quaternion.identity);
            if (_audioSource != null)
                _audioSource.Play();
            Destroy(gameObject); 
        }
        else
        {
            GameManager.Instance._isReturnToAngar = true;
            SceneManager.LoadScene("Upgrade_Menu");
            if (_audioSource != null)
                _audioSource.Play();
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
