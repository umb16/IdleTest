using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBusiness : MonoBehaviour
{
    [SerializeField] private UIUpgradeButton _upgradePrefab;
    [SerializeField] private Transform _upgradeRoot;

    [SerializeField] private TMP_Text _name;

    [SerializeField] private UIProgressBar _progressBar;

    [SerializeField] private Button _levelUpButton;
    [SerializeField] private TMP_Text _levelUpButtonText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _profitText;

    private Business _business;

    public void Set(Business business, BusinessData businessData, NamesData namesData)
    {
        _name.text = namesData.GetName(businessData.Name);
        _business = business;
        for (int i = 0; i < businessData.Upgrades.Count; i++)
        {
            UpgradeData upgrade = businessData.Upgrades[i];
            UIUpgradeButton uiUpgrade = Instantiate(_upgradePrefab, _upgradeRoot);
            uiUpgrade.Set(namesData.GetName(upgrade.Name), upgrade.ProfitMultiplier, upgrade.Cost, business.GetUpgrade(i));
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_upgradeRoot);

        _levelUpButton.onClick.AddListener(_business.LevelUp);
        UniTaskAsyncEnumerable.EveryValueChanged(_business, x => x.LevelUpIsAvaliable).Subscribe(OnLevelUpStatusChanged);
        UniTaskAsyncEnumerable.EveryValueChanged(_business, x => x.Profit).Subscribe(OnProfitChanged);
        UniTaskAsyncEnumerable.EveryValueChanged(_business, x => x.Level).Subscribe(_ => OnLevelChanged());
    }

    private void OnLevelChanged()
    {
        _levelText.text = "LVL\n" +
                          _business.Level;
        _levelUpButtonText.text = "LVL UP\n" +
                                  "Цена: $" + _business.LevelUpCost;
    }

    private void OnProfitChanged(int profit)
    {
        _profitText.text = "Доход" +
                   _business.Profit;
    }

    private void OnLevelUpStatusChanged(bool levelUpAvaliable)
    {
        _levelUpButton.interactable = levelUpAvaliable;
    }

    private void Update()
    {
        _progressBar.SetValue(_business.DelayProgressNormalized);
    }
}
