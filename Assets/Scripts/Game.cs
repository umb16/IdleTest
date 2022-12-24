using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private BusinnessListData _businnessListData;
    [SerializeField] private NamesData _namesData;

    [SerializeField] private UIBusiness _bussinesPrefab;
    [SerializeField] private Transform _businessesRoot;

    private List<BusinessPair> _businesses = new List<BusinessPair>();

    [SerializeField] private TMP_Text _balanceText;
    private SpecialProperty<int> _balance = new SpecialProperty<int>(0);

    private void Awake()
    {
        _balance.Changed += BlanceChanged;
        _businnessListData = Instantiate(_businnessListData);
        _namesData = Instantiate(_namesData);
        foreach (var businessData in _businnessListData.BusinessDatas)
        {
            Business business = new Business(businessData.BaseCost, businessData.BaseProfit,
                businessData.Delay, businessData.Upgrades, _balance);

            UIBusiness uIBusiness = Instantiate(_bussinesPrefab, _businessesRoot);
            uIBusiness.Set(business, businessData,  _namesData);
            _businesses.Add(new BusinessPair(business, uIBusiness));
        }

    }
    private void BlanceChanged(int balance)
    {
        _balanceText.text = "$" + balance;
    }
}
