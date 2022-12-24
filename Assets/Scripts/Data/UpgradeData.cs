using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeData
{
    [field: SerializeField] public NameKey Name { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public int ProfitMultiplier { get; private set; }
    public float RealMultiplier => ProfitMultiplier * .01f + 1;
}
