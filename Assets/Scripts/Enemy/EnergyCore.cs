using UnityEngine;

public class EnergyCore : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _amount = 1;

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
            GameManager.Instance.AddMoney(_amount);
            Destroy(gameObject);
        }
    }
}
