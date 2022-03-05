using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ElementGaugeUI : IComponentData
{
    public Entity Target { get; set; }
    public ElementDataGroup ElementData { get; set; }
    public RectTransform ResourceHolder { get; set; }
}

public class ElementGaugeUIComponent : ManagedComponentAuthoring<ElementGaugeUI>
{
    [SerializeField] private EntityRef _target;
    [SerializeField] private ElementDataGroup _elementData;
    [SerializeField] private RectTransform _resourceHolder;
    protected override ElementGaugeUI AuthorComponent(World world)
    {
        return new ElementGaugeUI() { Target = _target.Entity, ElementData = _elementData, ResourceHolder = _resourceHolder };
    }
}
