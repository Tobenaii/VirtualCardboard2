using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributePanelScaler : MonoBehaviour
{
    [SerializeField] private EntityRef _entity;
    [SerializeField] private Attribute _attribute;
    [SerializeField] private float _maxWidth;
    [SerializeField] private RectTransform _transform;

    private void Start()
    {
        _attribute.RegisterCallback(_entity.Instance, OnValueChanged);
        OnValueChanged(_attribute[_entity.Instance]);
    }

    private void OnValueChanged(float value)
    {
        float maxValue = _attribute.GetMaxValue(_entity.Instance);
        float width = _maxWidth * (value / maxValue);
        _transform.sizeDelta = new Vector2(width, _transform.sizeDelta.y);
    }
}
