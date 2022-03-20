using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ElementGaugeUI : IComponentData
{
    public ElementDataGroup ElementData { get; set; }
    public RectTransform ResourceHolder { get; set; }
}

public class ElementGaugeUIComponent : ComponentAuthoringBase
{
    [SerializeField] private ElementDataGroup _elementData;
    [SerializeField] private RectTransform _resourceHolder;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new ElementGaugeUI() { ElementData = _elementData, ResourceHolder = _resourceHolder });
    }
}
