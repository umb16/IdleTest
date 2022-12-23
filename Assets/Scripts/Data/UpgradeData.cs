using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeData
{
    public NameKey Name;
    [Header("Цена улучшения")]
    public int Cost;
    [Header("Множитель в процентах")]
    public int ProfitMultiplier;
    public float RealMultiplier => ProfitMultiplier * .01f + 1;
}
