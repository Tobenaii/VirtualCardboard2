using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HealthBar : MonoBehaviour, IComponentListener<Stat>
{
    [SerializeField] private EntityRef _entity;
    [SerializeField] private ComponentEvent<Stat, Stat.Type> _healthEvent;

    [SerializeField] private RectTransform _healthBar;

    private float _maxWidth;
    private float _velocity;
    private float _target;

    private void Start()
    {
        _healthEvent.Register(_entity.Entity, Stat.Type.Health, this);
        _maxWidth = _healthBar.sizeDelta.x;
        _target = _maxWidth;
    }

    private void Update()
    {
        _healthBar.sizeDelta = new Vector2(Mathf.SmoothDamp(_healthBar.sizeDelta.x, _target, ref _velocity, 0.1f), _healthBar.sizeDelta.y);
    }

    public void OnComponentChanged(Stat value)
    {
        var width = _maxWidth * (value.CurrentValue / value.MaxValue);
        _target = width;
    }
}
