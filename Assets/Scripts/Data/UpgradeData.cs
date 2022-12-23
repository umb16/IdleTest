using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeData
{
    public NameKey Name;
    [Header("���� ���������")]
    public int Cost;
    [Header("��������� � ���������")]
    public int ProfitMultiplier;
    public float RealMultiplier => ProfitMultiplier * .01f + 1;
}
