using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    //—тоит на месте и периодически выпускает лазеры
    BossLazer bossLazer;

    [SerializeField] float amplitude = 2f;
    [SerializeField] float frequency = 1f;

    private float elapsedTime;

    private void Awake()
    {
        bossLazer = GetComponent<BossLazer>();
    }

    private void Update()
    {
        bossLazer.Attack();

        elapsedTime += Time.deltaTime;
        float offsetX = Mathf.Sin(elapsedTime * frequency) * amplitude;
        transform.position = new Vector2(offsetX, transform.position.y);
    }
}
