using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HealthBar : MonoBehaviour, IComponentListener<Health>
{
    [SerializeField] private EntityRef _entity;
    [SerializeField] private HealthChangeEvent _healthEvent;

    [SerializeField] private RectTransform _healthBar;

    private float _maxWidth;

    private void Start()
    {
        _healthEvent.Register(_entity.Entity, this);
        _maxWidth = _healthBar.sizeDelta.x;
    }

    public void OnComponentChanged(Health prevHealth, Health newHealth)
    {
        var width = _maxWidth * (newHealth.Value / newHealth.MaxValue);
        _healthBar.sizeDelta = new Vector2(width, _healthBar.sizeDelta.y);
    }
}
