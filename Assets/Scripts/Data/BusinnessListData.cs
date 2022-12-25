using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StartBusinessData
{
    [field: SerializeField] public BusinessData Data { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
}

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
