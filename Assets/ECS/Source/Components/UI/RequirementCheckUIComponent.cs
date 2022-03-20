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

public class RequirementCheckUIComponent : ComponentAuthoringBase
{
    [SerializeField] private EntityData _action;
    [SerializeField] private CanvasGroup _blockingGroup;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new RequirementCheckUI() { BlockingGroup = _blockingGroup, Prefab = _action.GetPrefab(dstManager) });
    }
}
