using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class StatBar : MonoBehaviour, IComponentListener<IStat>
{
    [SerializeField] private EntityRef _entity;
    [SerializeField] private ComponentEvent<IStat> _event;

    [SerializeField] private RectTransform _bar;

    private float _maxWidth;
    private float _velocity;
    private float _target;

    private void Start()
    {
        _event.Register(_entity.Entity, this);
        _maxWidth = _bar.sizeDelta.x;
        _target = _maxWidth;
    }

    public void OnComponentChanged(IStat newStat)
    {
        var width = _maxWidth * ((float)newStat.CurrentValue / newStat.MaxValue);
        _target = width;
    }

    private void Update()
    {
        _bar.sizeDelta = new Vector2(Mathf.SmoothDamp(_bar.sizeDelta.x, _target, ref _velocity, 0.1f), _bar.sizeDelta.y);
    }
}
