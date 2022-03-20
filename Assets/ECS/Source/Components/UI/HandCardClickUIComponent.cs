using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HandCardClickUI : IComponentData
{
    public UIEvents CardClickEvent { get; set; }
}

public class HandCardClickUIComponent : ComponentAuthoringBase
{
    [SerializeField] private UIEvents _cardClickEvent;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new HandCardClickUI() { CardClickEvent = _cardClickEvent });
    }
}
