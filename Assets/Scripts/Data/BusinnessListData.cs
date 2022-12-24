using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BusinessList", menuName = "Data/BusinessList")]
public class BusinnessListData : ScriptableObject
{
    [SerializeField] private BusinessData[] _businessDatas;
    private IList<BusinessData> _businessDatasReadOnly;
    public IList<BusinessData> BusinessDatas
    {
        get
        {
            if (_businessDatasReadOnly == null)
                _businessDatasReadOnly = Array.AsReadOnly(_businessDatas);
            return _businessDatasReadOnly;
        }
    }
}
