using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BusinessData", menuName = "Data/Business")]
public class BusinessData : ScriptableObject
{
    [field: SerializeField] public NameKey Name { get; private set; }
    [field: SerializeField] public int Delay { get; private set; }
    [field: SerializeField] public int BaseCost { get; private set; }
    [field: SerializeField] public int BaseProfit { get; private set; }

    [SerializeField] private UpgradeData[] _upgrades;

    private IList<UpgradeData> _upgradesReadOnly;
    public IList<UpgradeData> Upgrades
    {
        get
        {
            if (_upgradesReadOnly == null)
                _upgradesReadOnly = Array.AsReadOnly(_upgrades);
            return _upgradesReadOnly;
        }
    }
}
