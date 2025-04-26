using UnityEngine;

public class CommonEnemy : BaseEnemy
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float minFireRate;
    [SerializeField] float maxFireRate;
    [SerializeField] float dragRate;
    [SerializeField] AudioSource EnemyFire;
    float fireRate;
    
    private float fireCooldown;

    protected override void Update()
    {
        base.Update();
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Fire();
            fireRate = Random.Range(minFireRate, maxFireRate);
            fireCooldown = fireRate;
        }
    }

    public override void Fire()
    {
        //EnemyFire.Play();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BaseBullet>()._isFriendly = false;
        bullet.GetComponent<BaseBullet>().Attack();
    }
}
