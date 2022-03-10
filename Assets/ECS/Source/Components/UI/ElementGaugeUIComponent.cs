using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ElementGaugeUI : IComponentData
{
    public ElementDataGroup ElementData { get; set; }
    public RectTransform ResourceHolder { get; set; }
}

public class ElementGaugeUIComponent : ManagedComponentAuthoring<ElementGaugeUI>
{
    [SerializeField] private ElementDataGroup _elementData;
    [SerializeField] private RectTransform _resourceHolder;
    protected override ElementGaugeUI AuthorComponent(World world)
    {
        return new ElementGaugeUI() { ElementData = _elementData, ResourceHolder = _resourceHolder };
    }
}
