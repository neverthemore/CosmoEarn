using UnityEngine;

public class CommonEnemy : BaseEnemy
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float maxFireRate;
    [SerializeField] float dragRate;
    float fireRate;

    private float fireCooldown;
    public override void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BaseBullet>()._isFriendly = false;
        bullet.GetComponent<BaseBullet>().Attack();
    }

    public void Drag()
    {
        
    }
    public override void Update()
    {
        base.Update();
        fireRate = Random.Range(1, maxFireRate);
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Fire();
            fireCooldown = fireRate;
        }
    }
}
