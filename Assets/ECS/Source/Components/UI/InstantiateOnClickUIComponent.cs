using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

public class InstantiateOnClickUI : IComponentData
{
    public UIEvents ClickEvent { get; set; }
    public List<Entity> Entities { get; set; }
}

public class InstantiateOnClickUIComponent : ManagedComponentAuthoring<InstantiateOnClickUI>
{
    [SerializeField] private UIEvents _clickEvent;
    [SerializeField] private List<ModEntity> _entities;

    protected override InstantiateOnClickUI AuthorComponent(World world)
    {
        //TODO: Yucky linq for runtime stuff
        return new InstantiateOnClickUI() { Entities = _entities.Select(x => x.GetPrefab(world.EntityManager, x.name)).ToList(), ClickEvent = _clickEvent };
    }
}
