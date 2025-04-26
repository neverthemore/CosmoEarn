using Unity.VisualScripting;
using UnityEngine;

public class BossLazer : MonoBehaviour
{
    [SerializeField] GameObject lazerPrefab;
    [SerializeField] Transform[] shotPoint;

    float _attackCooldown = 5f;
    float _currentCooldown = 0;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Attack()
    {
        _currentCooldown += Time.deltaTime;

        if (_currentCooldown >= _attackCooldown)
        {
            foreach (Transform t in shotPoint)
            {
                GameObject shot = Instantiate(lazerPrefab, t.transform.position, t.rotation);
                float newScaleY = Mathf.Lerp(
                transform.localScale.y,
                100f,
                10f * Time.deltaTime
                );

                shot.transform.localScale = new Vector2(
                    transform.localScale.x,
                    newScaleY
                );
                Destroy(shot, 1f);
            }
            _currentCooldown = 0f;
        }
    }
}
