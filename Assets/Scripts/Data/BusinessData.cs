using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BusinessData", menuName = "Data/Business")]
public class BusinessData : ScriptableObject
{
    public NameKey Name;
    public int Delay;
    public int BaseCost;
    public int BaseProfit;
    public UpgradeData[] Upgrades;

}
