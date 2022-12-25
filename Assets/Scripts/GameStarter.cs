using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private BusinnessListData _businnessListData;
    [SerializeField] private NamesData _namesData;

    [SerializeField] private UIBusiness _bussinesPrefab;
    [SerializeField] private Transform _businessesRoot;
    [SerializeField] private TMP_Text _balanceText;

    private Game _game;

    private void Awake()
    {
        _game = new Game();
        for (int i = 0; i < _businnessListData.BusinessDatas.Count; i++)
        {
            StartBusinessData businessData = _businnessListData.BusinessDatas[i];

            Business business = new Business(businessData.Data.BaseCost, businessData.Data.BaseProfit,
                businessData.Data.Delay, businessData.Data.Upgrades, businessData.Level, _game);
            _game.Businesses.Add(business);

            UIBusiness uIBusiness = Instantiate(_bussinesPrefab, _businessesRoot);
            uIBusiness.Set(business, businessData.Data, _namesData);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_businessesRoot);

        UniTaskAsyncEnumerable.EveryValueChanged(_game, x => x.Balance).Subscribe(OnBalanceChanged);
    }

    private void OnBalanceChanged(int balance)
    {
        _balanceText.text = "$" + _game.Balance;
    }

    private void Update()
    {
        foreach (var business in _game.Businesses)
        {
            business.Update(Time.deltaTime);
        }
    }
}
