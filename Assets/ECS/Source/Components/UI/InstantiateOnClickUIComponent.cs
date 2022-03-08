using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

public class InstantiateOnClickUI : IComponentData
{
    public Entity Dealer { get; set; }
    public UIEvents ClickEvent { get; set; }
    public List<Entity> Entities { get; set; }
}

public class InstantiateOnClickUIComponent : ManagedComponentAuthoring<InstantiateOnClickUI>
{
    [SerializeField] private EntityRef _dealer;
    [SerializeField] private UIEvents _clickEvent;
    [SerializeField] private List<ModEntity> _entities;

    protected override InstantiateOnClickUI AuthorComponent(World world)
    {
        //TODO: Yucky linq for runtime stuff
        return new InstantiateOnClickUI() { Dealer = _dealer.Entity, Entities = _entities.Select(x => x.GetPrefab(world.EntityManager, x.name)).ToList(), ClickEvent = _clickEvent };
    }
}
