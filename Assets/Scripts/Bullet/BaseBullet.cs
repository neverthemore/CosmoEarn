using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseBullet : MonoBehaviour
{
    [SerializeField]protected float bulletSpeed = 10f;
    [SerializeField]protected int bulletDamage = 1;


    public bool _isFriendly = false;

    //Тут нужно еще делать проверку, своя ли это пуля, чтобы себя не била
    protected Rigidbody2D rb;

    protected virtual void Awake()
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
        Destroy(gameObject, 4f);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null && (collision.gameObject.GetComponent<IDamageable>().IsFriendly() != _isFriendly))
        {
            
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(bulletDamage);
            
            gameObject.GetComponent<Collider2D>().enabled = false;
            rb.linearVelocity = Vector2.zero;


            int randomIndex = Random.Range(0, 2);

            Destroy(gameObject);
        }
        
    }
}
