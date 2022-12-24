using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBusiness : MonoBehaviour
{
    [SerializeField] private UIUpgradeButton _upgradePrefab;
    [SerializeField] private Transform _upgradeRoot;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _profitText;

    public void Set(Business business, BusinessData businessData, NamesData namesData)
    {
        _name.text = namesData.GetName(businessData.Name);
        for (int i = 0; i < businessData.Upgrades.Count; i++)
        {
            UpgradeData upgrade = businessData.Upgrades[i];
            UIUpgradeButton uiUpgrade = Instantiate(_upgradePrefab, _upgradeRoot);
            uiUpgrade.Set(namesData.GetName(upgrade.Name), upgrade.ProfitMultiplier, upgrade.Cost, null);
        }
    }
}
