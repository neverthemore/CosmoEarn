using UnityEngine;
using System.Collections.Generic;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] private List<BaseGun> unlockedGuns;

    private void Update()
    {
        foreach (var gun in unlockedGuns)
        {
            gun.Attack();
        }
    }
}
