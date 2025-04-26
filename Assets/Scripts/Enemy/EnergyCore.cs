using UnityEngine;
using UnityEngine.Audio;

public class EnergyCore : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _amount = 1;
    [SerializeField] AudioClip coinpickup;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
       
    }
    private void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(coinpickup);
            GameManager.Instance.AddMoney(_amount);        
            Destroy(gameObject);
        }
    }
}
