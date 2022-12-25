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
    public int Balance { get; private set; }

    private void Awake()
    {
        foreach (var businessData in _businnessListData.BusinessDatas)
        {
            Business business = new Business(businessData.BaseCost, businessData.BaseProfit,
                businessData.Delay, businessData.Upgrades, this);

            UIBusiness uIBusiness = Instantiate(_bussinesPrefab, _businessesRoot);
            uIBusiness.Set(business, businessData,  _namesData);
            _businesses.Add(new BusinessPair(business, uIBusiness));
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_businessesRoot);
    }
    public void AddBalance(int value)
    {
        Balance += value;
        _balanceText.text = "$" + Balance;
    }

    private void Update()
    {
        foreach (var pair in _businesses)
        {
            pair.Business.Update(Time.deltaTime);
        }
    }
}
