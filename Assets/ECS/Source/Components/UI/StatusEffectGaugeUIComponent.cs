using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class StatusEffectGaugeUI : IComponentData
{
    public Entity Target { get; set; }
    public ElementDataGroup ElementData { get; set; }
    public RectTransform ResourceHolder { get; set; }
}

public class StatusEffectGaugeUIComponent : ManagedComponentAuthoring<StatusEffectGaugeUI>
{
    [SerializeField] private EntityRef _target;
    [SerializeField] private ElementDataGroup _elementData;
    [SerializeField] private RectTransform _resourceHolder;

    protected override StatusEffectGaugeUI AuthorComponent(World world)
    {
        return new StatusEffectGaugeUI() { Target = _target.Entity, ElementData = _elementData, ResourceHolder = _resourceHolder };
    }
}

