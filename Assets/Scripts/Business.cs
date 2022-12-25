using System;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Business
{
    private int _baseLevelUpCost;
    private int _baseProfit;
    private Upgrade[] _upgrades;
    private Game _game;

    public int LevelUpCost => (Level + 1) * _baseLevelUpCost;
    public bool LevelUpIsAvaliable => LevelUpCost <= _game.Balance;
    public int Level { get; private set; }

    public float DelayProgressNormalized => DelayProgressInSeconds / ProfitDelay;
    public int ProfitDelay { get; private set; }
    public float DelayProgressInSeconds { get; private set; }
    public int Profit => GetProfit();

    public Business(int baseLevelUpCost, int baseProfit, int profitDelay,
        IList<UpgradeData> upgradeDatas, int startLevel, Game game)
    {
        Level = startLevel;
        _game = game;
        _baseLevelUpCost = baseLevelUpCost;
        _baseProfit = baseProfit;
        ProfitDelay = profitDelay;
        _upgrades = new Upgrade[upgradeDatas.Count];
        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            var upgrade = upgradeDatas[i];
            _upgrades[i] = new Upgrade(upgrade.Cost, upgrade.RealMultiplier, _game);
        }
    }

    public void SetLevel(int value)
    {
        Level = value;
    }
    public void LevelUp()
    {
        _game.AddBalance(-LevelUpCost);
        Level++;
    }

    public Upgrade GetUpgrade(int index)
    {
        return _upgrades[index];
    }
    
    public void Update(float deltaTime)
    {
        if (Level == 0)
            return;
        DelayProgressInSeconds += deltaTime;
        if (DelayProgressInSeconds >= ProfitDelay)
        {
            DelayProgressInSeconds -= ProfitDelay;
            _game.AddBalance(GetProfit());
        }
    }
    private int GetProfit()
    {
        float upgradesMul = _upgrades.Where(x => x.Purchased)
            .Select(x => x.Multiplier)
            .Aggregate(1f, (x, y) => x * y);
        return Mathf.RoundToInt(Level * _baseProfit * upgradesMul);
    }
}
