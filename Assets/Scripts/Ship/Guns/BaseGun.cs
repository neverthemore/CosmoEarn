using UnityEngine;

public class BaseGun : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;

    [SerializeField] protected Transform firePoint;

    [SerializeField] float fireRate = 1f;

    private float fireCooldown = 0f;
    [SerializeField] AudioSource FireSound;
    public virtual void Attack()
    {
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
