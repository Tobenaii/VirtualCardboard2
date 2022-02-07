using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HealthBar : MonoBehaviour, IComponentListener<Health>
{
    [SerializeField] private EntityRef _entity;
    [SerializeField] private HealthEvent _healthEvent;

    [SerializeField] private RectTransform _healthBar;

    private float _maxWidth;
    private float _velocity;
    private float _target;

    private void Start()
    {
        _healthEvent.Register(_entity.Entity, this);
        _maxWidth = _healthBar.sizeDelta.x;
        _target = _maxWidth;
    }

    public void OnComponentChanged(Health newHealth)
    {
        var width = _maxWidth * (newHealth.Value / newHealth.MaxValue);
        _target = width;
    }

    private void Update()
    {
        _healthBar.sizeDelta = new Vector2(Mathf.SmoothDamp(_healthBar.sizeDelta.x, _target, ref _velocity, 0.1f), _healthBar.sizeDelta.y);
    }
}
