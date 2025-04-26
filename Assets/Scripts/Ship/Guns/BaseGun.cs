using UnityEngine;

public class BaseGun : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;

    [SerializeField] protected Transform firePoint;

    [SerializeField] protected float fireRate = 1f;

    [SerializeField] protected UpgradeData upgrade;

    private float fireCooldown = 0f;
    [SerializeField] AudioSource FireSound;

    [SerializeField] private bool isPlayer = false;

    private void Start()
    {
        if (isPlayer)
        {
            fireRate = upgrade.GetCurrentValue();
        }
    }
    public virtual void Attack()
    {
        if (fireRate <= 0) return;
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Fire();
            fireCooldown = fireRate;
            
            FireSound?.Play();
        }
    }

    protected virtual void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BaseBullet>()._isFriendly = true;
        bullet.GetComponent<BaseBullet>().Attack();
        //Debug.Log("Атака");
    }
}
