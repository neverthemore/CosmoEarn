using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] int bulletDamage = 1;


    public bool _isFriendly = false;
    //��� ����� ��� ������ ��������, ���� �� ��� ����, ����� ���� �� ����
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Attack()
    {
        if (_isFriendly)
        {
            rb.linearVelocity = transform.up * bulletSpeed;
        }
        else
        {
            rb.linearVelocity = -transform.up * bulletSpeed;
        }
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null && (collision.gameObject.GetComponent<IDamageable>().IsFriendly() != _isFriendly))
        {
            
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(bulletDamage);
        }
        
    }
}
