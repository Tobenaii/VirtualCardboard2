using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : IComponentData, ISmoothDamp<float>
{
    public Entity Target { get; set; }
    public Image HealthBar { get; set; }

    public float SmoothTime { get; set; } = 0.1f;


    private float _velocity;

    public float SmoothDamp(float current, float target)
    {
        return Mathf.SmoothDamp(current, target, ref _velocity, SmoothTime);
    }
}

public class HealthBarUIComponent : ManagedComponentAuthoring<HealthBarUI>
{
    [SerializeField] private EntityRef _target;
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _smoothTime;
    protected override HealthBarUI AuthorComponent(World world)
    {
        return new HealthBarUI() { HealthBar = _healthBar, Target = _target.Entity, SmoothTime = _smoothTime };
    }
}
