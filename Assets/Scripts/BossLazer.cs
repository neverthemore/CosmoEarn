using Unity.VisualScripting;
using UnityEngine;

public class BossLazer : MonoBehaviour
{
    [SerializeField] GameObject lazerPrefab;
    [SerializeField] Transform shotPoint;
    void Start()
    {
        
    }
   
    void Update()
    {
        
    }

    public void Atack()
    {
        GameObject shot = Instantiate(lazerPrefab, shotPoint.position, shotPoint.rotation);
        float newScaleY = Mathf.Lerp(
            transform.localScale.y,
            100f,
            10f * Time.deltaTime
        );
        shot.transform.localScale = new Vector2(
            transform.localScale.x,
            newScaleY            
        );
        Destroy(shot);
    }
}
