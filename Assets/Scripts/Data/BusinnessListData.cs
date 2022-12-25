using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BusinessList", menuName = "Data/BusinessList")]
public class BusinnessListData : ScriptableObject
{
    [SerializeField] private StartBusinessData[] _businessDatas;
    private IList<StartBusinessData> _businessDatasReadOnly;
    public IList<StartBusinessData> BusinessDatas
    {
        get
        {
            if (_businessDatasReadOnly == null)
                _businessDatasReadOnly = Array.AsReadOnly(_businessDatas);
            return _businessDatasReadOnly;
        }
    }
}
