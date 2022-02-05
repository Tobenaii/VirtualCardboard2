using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeText : MonoBehaviour
{
    [SerializeField] private EntityRef _entity;
    [SerializeField] private Attribute _attribute;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private string _format;

    private void Start()
    {
        _attribute.RegisterCallback(_entity.Instance, OnValueChanged);
        OnValueChanged(_attribute[_entity.Instance]);
    }

    private void OnValueChanged(float value)
    {
        float maxValue = _attribute.GetMaxValue(_entity.Instance);
        var text = _format.Replace("{value}", value.ToString()).Replace("{maxValue}", maxValue.ToString());
        _text.text = text;
    }
}
