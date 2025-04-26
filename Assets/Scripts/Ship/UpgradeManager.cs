using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeData> upgrades;

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private TMP_Text Cost;
    [SerializeField] private TMP_Text LevelUpgrade;
    [SerializeField] private Image image;
    [SerializeField] private Button buyButton;

    [SerializeField] private TMP_Text Money;

    private UpgradeData currentUpgradeData;

    private void Start()
    {
        UpdateUI();
    }

    public void BuyUpgrade(UpgradeData upgrade)
    {
        if (upgrade.level < upgrade.maxLevel && GameManager.Instance.Money >= upgrade.GetCost())
        {
            if(GameManager.Instance.SpendMoney(upgrade.GetCost()))
            {
                upgrade.level++;
                UpdateUI();
                Debug.Log("Куплено улучшение");
            }
            

            //Ну и можно эффект например применить еще
        }
    }

    public void ShowUpgradeInfo(UpgradeData upgrade)
    {
        panel.SetActive(true);
        currentUpgradeData = upgrade;
        UpdateUI();
        /*
        currentUpgradeData = upgrade;
        Title.text = upgrade.upgradeName;
        Description.text = upgrade.description;
        image.sprite = upgrade.sprite;

        Cost.text = $"Стоимость: {upgrade.GetCost()}";
        LevelUpgrade.text = $"Текущий уровень: {upgrade.level}/{upgrade.maxLevel}";

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyUpgrade(upgrade));
        */
    }

    public void UpdateUI()
    {
        Money.text = GameManager.Instance.Money.ToString();
        if (currentUpgradeData == null) return;
        Title.text = currentUpgradeData.upgradeName;
        Description.text = currentUpgradeData.description;
        image.sprite = currentUpgradeData.sprite;

        Cost.text = $"Стоимость: {currentUpgradeData.GetCost()}";
        LevelUpgrade.text = $"Текущий уровень: {currentUpgradeData.level}/{currentUpgradeData.maxLevel}";

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyUpgrade(currentUpgradeData));
    }
}
