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

    private Action _onClick;

    private string _name;
    private string _multiplierText;
    private string _costText;

    public void Set(string name, float multiplier, int cost, Action onClick)
    {
        _name = name;
        _multiplierText = "Доход: +" + multiplier * 100 + "%";
        _costText = "Цена: $" + cost;
        _onClick = onClick;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClick);
        UpdateText(false);
    }

    public void SetDisabled()
    {
        _button.interactable = false;
        UpdateText(false);
    }

    public void SetEnabled()
    {
        _button.interactable = true;
        UpdateText(false);
    }

    public void SetPurchased()
    {
        _button.interactable = false;
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

    private void OnClick()
    {
        _onClick.Invoke();
    }

}
