using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Business
{
    private int _baseLevelUpCost;
    private int _baseProfit;
    public int Level { get; private set; }
    public int LevelUpCost => (Level + 1) * _baseLevelUpCost;
    public int ProfitDelay { get; private set; }

    public float DelayProgress { get; private set; }

    private Upgrade[] _upgrades;

    public int GetRevenue()
    {
        float upgradesMul = _upgrades.Where(x => x.Purchased)
            .Select(x => x.Multiplier)
            .Aggregate(1f, (x, y) => x * y);
        return Mathf.RoundToInt(Level * _baseProfit * upgradesMul);
    }

    public bool LevelUpIsAvaliable(int balance) => LevelUpCost <= balance;
    public UpgradeStatus GetUpgradeStatus(int index, int balance)
    {
        Upgrade upgrade = _upgrades[index];
        if (_upgrades[index].Purchased)
        {
            return UpgradeStatus.Purchased;
        }
        if (upgrade.Cost <= balance)
        {
            return UpgradeStatus.AvailableForPurchase;
        }
        return UpgradeStatus.NotAvaliable;
    }
    public void Update(float deltaTime)
    {
        DelayProgress += deltaTime;
        if (DelayProgress >= ProfitDelay)
        {
            DelayProgress -= ProfitDelay;
        }
    }
}
