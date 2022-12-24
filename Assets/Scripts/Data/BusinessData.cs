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
    [field: SerializeField] public UpgradeData[] Upgrades { get; private set; }

}
