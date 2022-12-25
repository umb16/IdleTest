using System;
using UnityEngine;

[Serializable]
public class StartBusinessData
{
    [field: SerializeField] public BusinessData Data { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
}
