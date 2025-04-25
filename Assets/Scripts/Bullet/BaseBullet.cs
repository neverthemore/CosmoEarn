using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] int bulletDamage = 1;

    [SerializeField] Animator[] animator;
    [SerializeField] GameObject bulletSprite;


    public bool _isFriendly = false;

    private bool _isExplosed = false;
    //Тут нужно еще делать проверку, своя ли это пуля, чтобы себя не била
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach (var animator in animator)
        {
            animator.gameObject.SetActive(false);
        }
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
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null && (collision.gameObject.GetComponent<IDamageable>().IsFriendly() != _isFriendly))
        {
            
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(bulletDamage);

            bulletSprite.SetActive(false);
            gameObject.GetComponent<Collider2D>().enabled = false;
            rb.linearVelocity = Vector2.zero;


            int randomIndex = Random.Range(0, 2);

            animator[randomIndex].gameObject.SetActive(true);
            animator[randomIndex].SetTrigger("Boom");
            Destroy(gameObject, 0.5f);
        }
        
    }
}
