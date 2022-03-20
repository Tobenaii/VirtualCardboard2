using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class StatusEffectGaugeUI : IComponentData
{
    public ElementDataGroup ElementData { get; set; }
    public RectTransform ResourceHolder { get; set; }
}

public class StatusEffectGaugeUIComponent : ComponentAuthoringBase
{
    [SerializeField] private ElementDataGroup _elementData;
    [SerializeField] private RectTransform _resourceHolder;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new StatusEffectGaugeUI() { ElementData = _elementData, ResourceHolder = _resourceHolder });
    }
}

