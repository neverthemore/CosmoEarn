using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private float forceMultiplier = 5f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float damping = 0.98f;
    [SerializeField] private float randomWiggleStrength = 1f;
    [SerializeField] private float targetHoldRadius = 0.3f;

    private Rigidbody2D rb;
    private Vector2 targetPosition;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        MoveToPositoin();
    }

   

    virtual public void Fire()
    {
        

    }

    public virtual void SetTargetPosition(Vector2 targetPos)
    {
        targetPosition = targetPos;
    }

    private void MoveToPositoin()
    {
        Vector2 toTarget = targetPosition - rb.position;
        float distance = toTarget.magnitude;

        Vector2 desiredForce = Vector2.zero;

        if (distance > targetHoldRadius)
        {
            // Чем ближе враг к цели, тем слабее сила
            float adjustedForce = Mathf.Lerp(0, forceMultiplier, distance / 2f); // 2f — дистанция, на которой сила максимальная
            desiredForce = toTarget.normalized * adjustedForce;
        }
        else
        {
            // Когда враг уже на месте — лёгкое покачивание, чтобы не стоял как камень
            float wiggleX = Mathf.PerlinNoise(Time.time * 0.5f, transform.position.y) - 0.5f;
            float wiggleY = Mathf.PerlinNoise(transform.position.x, Time.time * 0.5f) - 0.5f;
            Vector2 wiggle = new Vector2(wiggleX, wiggleY) * randomWiggleStrength;
            desiredForce = wiggle;
        }

        // Применяем рассчитанную силу
        rb.AddForce(desiredForce);

        // Ограничиваем скорость
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        // Немного гасим скорость для стабилизации
        rb.linearVelocity *= damping;
    }
}
