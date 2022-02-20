using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour, IComponentListener<IStat>
{
    [SerializeField] private EntityRef _entity;
    [SerializeField] private ComponentEvent<IStat> _event;

    [SerializeField] private Image _bar;

    private float _velocity;
    private float _target;

    private void Start()
    {
        _event.Register(_entity.Entity, this);
    }

    public void OnComponentChanged(IStat newStat)
    {
        _target = (float)newStat.CurrentValue / newStat.MaxValue;
    }

    private void Update()
    {
        _bar.fillAmount = Mathf.SmoothDamp(_bar.fillAmount, _target, ref _velocity, 0.1f);
    }
}
