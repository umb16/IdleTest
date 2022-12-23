using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProgressBar : MonoBehaviour
{
    [SerializeField] private Transform _barTransform;

    public void SetValue(float value)
    {
        _barTransform.localScale = new Vector3(value, 1f, 1f);
    }
}
