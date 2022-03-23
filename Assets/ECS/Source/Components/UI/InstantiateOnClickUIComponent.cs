using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ActionButtonUI : IComponentData
{
    public UIEvents ClickEvent { get; set; }
}

[MovedFrom(true, sourceClassName: "InstantiateOnClickUIComponent")]
public class ActionButtonUIComponent : ComponentAuthoringBase
{
    [SerializeField] private UIEvents _clickEvent;
    [SerializeField] private EntityData _action;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        _action.Convert(entity, dstManager);
        dstManager.AddComponentData(entity, new ActionButtonUI() { ClickEvent = _clickEvent });
    }
}
