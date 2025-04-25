using UnityEngine;
using System.Collections.Generic;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] private List<BaseGun> unlockedGuns;
    [SerializeField] bool _isShoot = true;


    private void Update()
    {
        if (!_isShoot) return;
        foreach (var gun in unlockedGuns)
        {
            gun.Attack();       
        }
    }
}
