using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 0.2f;

    private float fireCooldown;

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Fire();
            fireCooldown = fireRate;
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BaseBullet>()._isFriendly = true;
        bullet.GetComponent<BaseBullet>().Attack();
    }
}
