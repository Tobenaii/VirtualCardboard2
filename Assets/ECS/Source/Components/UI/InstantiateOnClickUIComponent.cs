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

public class InstantiateOnClickUIComponent : ManagedComponentAuthoring<InstantiateOnClickUI>
{
    [SerializeField] private UIEvents _clickEvent;
    [SerializeField] private EntityData _action;

    protected override InstantiateOnClickUI AuthorComponent(World world)
    {
        //TODO: Yucky linq for runtime stuff
        return new InstantiateOnClickUI() { Action = _action.GetPrefab(world.EntityManager), ClickEvent = _clickEvent };
    }
}
