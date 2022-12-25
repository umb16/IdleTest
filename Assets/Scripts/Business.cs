using System;

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
    public float DelayProgressInSeconds { get; private set; }
    public float DelayProgressNormalized => DelayProgressInSeconds / ProfitDelay;

    private Upgrade[] _upgrades;
    private Game _game;

    public Business(int baseLevelUpCost, int baseProfit, int profitDelay,
        IList<UpgradeData> upgradeDatas, Game game)
    {
        _game = game;
        _baseLevelUpCost = baseLevelUpCost;
        _baseProfit = baseProfit;
        ProfitDelay = profitDelay;
        _upgrades = new Upgrade[upgradeDatas.Count];
        for (int i = 0; i < upgradeDatas.Count; i++)
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
        _game.AddBalance(-LevelUpCost);
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
        DelayProgressInSeconds += deltaTime;
        if (DelayProgressInSeconds >= ProfitDelay)
        {
            DelayProgressInSeconds -= ProfitDelay;
        }
    }
}
