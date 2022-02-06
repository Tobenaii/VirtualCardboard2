using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HealthBar : IManagedComponentAuthoring<HealthBar, HealthBarComponent>
{
    public RectTransform transform;
    public float maxWidth;
    public Entity entity;
}

public class HealthBarComponent : UnityHookAuthoring<HealthBar>
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private EntityRef _entity;
    [SerializeField] private float _maxWidth;

    protected override HealthBar AuthorComponent(World world)
    {
        return new HealthBar() { transform = _transform, maxWidth = _maxWidth, entity = _entity.Entity };
    }
}
