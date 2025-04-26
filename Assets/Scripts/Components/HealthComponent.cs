using System.Linq;
using UnityEditor.Tilemaps;
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
            _audioSource.Play();
        }
        else
        {
            GameManager.Instance._isReturnToAngar = true;
            _audioSource.Play();
            SceneManager.LoadScene("Upgrade_Menu");
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
