using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HandCardClickUI : IComponentData
{
    public Entity Target { get; set; }
    public UIEvents CardClickEvent { get; set; }
}

public class HandCardClickUIComponent : ManagedComponentAuthoring<HandCardClickUI>
{
    [SerializeField] private EntityRef _target;
    [SerializeField] private UIEvents _cardClickEvent;
    protected override HandCardClickUI AuthorComponent(World world)
    {
        return new HandCardClickUI() { CardClickEvent = _cardClickEvent, Target = _target.Entity };
    }
}
