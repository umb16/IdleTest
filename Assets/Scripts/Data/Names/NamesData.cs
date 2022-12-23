using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NamesData", menuName = "Data/NamesData")]
public class NamesData : ScriptableObject
{
    [SerializeField] private NameData[] _data;
    private Dictionary<NameKey, string> _names;

    public string GetName(NameKey key)
    {
        if (_names.TryGetValue(key, out string name))
        {
            return name;
        }
        return key.ToString();
    }

    private void OnValidate()
    {
        if (_data.Select(x => x.NameKey).Distinct().Count() < _data.Length)
        {
            Debug.LogError("NamesData " + name + "содержит постор€ющиес€ элементы");
        }
    }
    private void Awake()
    {
        if (_data != null)
            _names = _data.ToDictionary(x => x.NameKey, x => x.Name);
    }
}
