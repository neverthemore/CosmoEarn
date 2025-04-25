using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeData> upgrades;

    public void BuyUpgrade(UpgradeData upgrade)
    {
        if (upgrade.level < upgrade.maxLevel && GameManager.Instance.Money >= upgrade.GetCost())
        {
            GameManager.Instance.SpendMoney(upgrade.GetCost());
            upgrade.level++;

            //Ну и можно эффект например применить еще
        }
    }

    
}
