using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

public class InstantiateOnClickUI : IComponentData
{
    public UIEvents ClickEvent { get; set; }
    public Entity Action { get; set; }
}

public class InstantiateOnClickUIComponent : ComponentAuthoringBase
{
    [SerializeField] private UIEvents _clickEvent;
    [SerializeField] private EntityData _action;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        //TODO: Yucky linq for runtime stuff
        dstManager.AddComponentData(entity, new InstantiateOnClickUI() { Action = _action.GetPrefab(dstManager), ClickEvent = _clickEvent });
    }
}
