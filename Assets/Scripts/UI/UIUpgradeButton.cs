using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIUpgradeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private Color _purchasedColor;
    [SerializeField] private Color _diasabledColor;

    private string _name;
    private string _multiplierText;
    private string _costText;

    public void Set(string name, int multiplier, int cost, Upgrade upgrade)
    {
        _name = name;
        _multiplierText = "Доход: +" + multiplier + "%";
        _costText = "Цена: $" + cost;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(upgrade.PurchaseUpgrade);
        UniTaskAsyncEnumerable.EveryValueChanged(upgrade, x => x.Status).Subscribe(SetStatus);
    }

    private void SetStatus(UpgradeStatus status)
    {
        switch (status)
        {
            case UpgradeStatus.Purchased:
                SetPurchased();
                break;
            case UpgradeStatus.AvailableForPurchase:
                SetEnabled();
                break;
            default:
                SetDisabled();
                break;
        }
    }

    private void SetDisabled()
    {
        _button.interactable = false;

        var colors = _button.colors;
        colors.disabledColor = _diasabledColor;
        _button.colors = colors;

        UpdateText(false);
    }

    private void SetEnabled()
    {
        _button.interactable = true;
        UpdateText(false);
    }

    private void SetPurchased()
    {
        _button.interactable = false;
        var colors = _button.colors;
        colors.disabledColor = _purchasedColor;
        _button.colors = colors;
        UpdateText(true);
    }

    private void UpdateText(bool purchased)
    {
        if (purchased)
        {
            _buttonText.text = _name + "\n" +
                               _multiplierText +
                               "\n" + "Куплено";
        }
        else
        {
            _buttonText.text = _name + "\n" +
                               _multiplierText +
                               "\n" + _costText;
        }
    }
}
