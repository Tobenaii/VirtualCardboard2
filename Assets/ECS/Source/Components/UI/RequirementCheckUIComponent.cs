using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

public class RequirementCheckUI : IComponentData
{
    public Entity Prefab { get; set; }
    public Entity Requirement { get; set; }
    public CanvasGroup BlockingGroup { get; set; }
}

public class RequirementCheckUIComponent : ManagedComponentAuthoring<RequirementCheckUI>
{
    [SerializeField] private EntityData _action;
    [SerializeField] private CanvasGroup _blockingGroup;

    protected override RequirementCheckUI AuthorComponent(World world)
    {
        return new RequirementCheckUI() { BlockingGroup = _blockingGroup, Prefab = _action.GetPrefab(world.EntityManager) };
    }
}
