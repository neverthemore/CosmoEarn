using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] int bulletDamage = 1;

    public bool _isFriendly = false;
    //Тут нужно еще делать проверку, своя ли это пуля, чтобы себя не била
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Attack()
    {
        rb.linearVelocity = Vector2.up * bulletSpeed;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null && (collision.gameObject.GetComponent<IDamageable>().IsFriendly() != _isFriendly))
        {
            
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(bulletDamage);
        }
        
    }
}
