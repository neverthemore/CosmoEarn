using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;
    public int baseCost;
    public int level;
    public int maxLevel;

    public float[] valuesPerLevel; //�������� [1.0, 1.2, 1.4 � ��]

    public int GetCost() => baseCost * (level + 1);
    public float GetCurrentValue() => valuesPerLevel[level];
}
