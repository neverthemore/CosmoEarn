using UnityEngine;

public class LazerBullet : BaseBullet
{
    [SerializeField] Animator[] animator;
    [SerializeField] GameObject bulletSprite;

    protected override void Awake()
    {
        base.Awake();
        if (animator == null) return;
        foreach (var animator in animator)
        {

            animator.gameObject.SetActive(false);
        }
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<IDamageable>() != null && (collision.gameObject.GetComponent<IDamageable>().IsFriendly() != _isFriendly))
        {

            collision.gameObject.GetComponent<IDamageable>().TakeDamage(bulletDamage);

            if (bulletSprite != null)
            {
                bulletSprite.SetActive(false);
            }

            gameObject.GetComponent<Collider2D>().enabled = false;
            rb.linearVelocity = Vector2.zero;


            int randomIndex = Random.Range(0, 2);

            if (animator != null)
            {
                animator[randomIndex]?.gameObject.SetActive(true);
                animator[randomIndex]?.SetTrigger("Boom");
            }

            Destroy(gameObject, 0.5f);
        }

    }
}

