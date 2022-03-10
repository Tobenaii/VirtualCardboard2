using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class StatusEffectGaugeUI : IComponentData
{
    public ElementDataGroup ElementData { get; set; }
    public RectTransform ResourceHolder { get; set; }
}

public class StatusEffectGaugeUIComponent : ManagedComponentAuthoring<StatusEffectGaugeUI>
{
    [SerializeField] private ElementDataGroup _elementData;
    [SerializeField] private RectTransform _resourceHolder;

    protected override StatusEffectGaugeUI AuthorComponent(World world)
    {
        return new StatusEffectGaugeUI() { ElementData = _elementData, ResourceHolder = _resourceHolder };
    }
}

