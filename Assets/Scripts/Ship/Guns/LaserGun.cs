using UnityEngine;
using System.Collections.Generic;

public class LaserGun : BaseGun
{
    //Имеет две точки спавна пуль
    [SerializeField] private List<Transform> firePoints;

    private int _currentPosIndex = 0;

    protected override void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoints[_currentPosIndex].position, firePoint.rotation);
        if (++_currentPosIndex >= firePoints.Count)
        {
            _currentPosIndex = 0;
        }
        bullet.GetComponent<BaseBullet>()._isFriendly = true;
        bullet.GetComponent<BaseBullet>().Attack();
    }
}
