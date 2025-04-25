using UnityEngine;

public class CommonEnemy : BaseEnemy
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float maxFireRate;
    [SerializeField] float dragRate;
    float fireRate;

    private float fireCooldown;

    protected override void Update()
    {
        base.Update();
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Fire();
            fireRate = Random.Range(1, maxFireRate);
            fireCooldown = fireRate;
        }
    }

    public override void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BaseBullet>()._isFriendly = false;
        bullet.GetComponent<BaseBullet>().Attack();
    }
}
