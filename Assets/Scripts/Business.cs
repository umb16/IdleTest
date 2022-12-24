using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Business
{
    private int _baseLevelUpCost;
    private int _baseProfit;

    public event Action LvlUp;
    public event Action<int, UpgradeStatus> UpgradeStatusChanged;
    public int Level { get; private set; }
    public int LevelUpCost => (Level + 1) * _baseLevelUpCost;
    public int ProfitDelay { get; private set; }

    private SpecialProperty<int> _balance;

    public float DelayProgress { get; private set; }

    private Upgrade[] _upgrades;

    public Business(int baseLevelUpCost, int baseProfit, int profitDelay, UpgradeData[] upgradeDatas, SpecialProperty<int> balance)
    {
        _balance = balance;
        _balance.Changed += BalanceChanged;
        _baseLevelUpCost = baseLevelUpCost;
        _baseProfit = baseProfit;
        ProfitDelay = profitDelay;
        _upgrades = new Upgrade[upgradeDatas.Length];
        for (int i = 0; i < upgradeDatas.Length; i++)
        {
            var upgrade = upgradeDatas[i];
            _upgrades[i] = new Upgrade(upgrade.Cost, upgrade.RealMultiplier);
        }
    }

    private void BalanceChanged(int balance)
    {
        for (int i = 0; i < _upgrades.Length; i++)
        {
            Upgrade upgrade = _upgrades[i];
            var status = GetUpgradeStatus(i, balance);
            if (upgrade.Status != status)
            {
                upgrade.Status = status;
                UpgradeStatusChanged.Invoke(i, status);
            }
        }
    }

    public int GetProfit()
    {
        float upgradesMul = _upgrades.Where(x => x.Status == UpgradeStatus.Purchased)
            .Select(x => x.Multiplier)
            .Aggregate(1f, (x, y) => x * y);
        return Mathf.RoundToInt(Level * _baseProfit * upgradesMul);
    }

    public void LevelUp()
    {
        _balance.Value -= LevelUpCost;
        Level++;
        LvlUp.Invoke();
    }

    public bool LevelUpIsAvaliable(int balance) => LevelUpCost <= balance;
    public UpgradeStatus GetUpgradeStatus(int index, int balance)
    {
        Upgrade upgrade = _upgrades[index];
        if (_upgrades[index].Status == UpgradeStatus.Purchased)
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
