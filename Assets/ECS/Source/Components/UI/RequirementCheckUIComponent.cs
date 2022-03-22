using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

public class RequirementCheckUI : IComponentData
{
    public Entity Requirement { get; set; }
    public CanvasGroup BlockingGroup { get; set; }
}

public class RequirementCheckUIComponent : ComponentAuthoringBase
{
    [SerializeField] private EntityRef _checkOn;
    [SerializeField] private EntityData _action;
    [SerializeField] private CanvasGroup _blockingGroup;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        var action = _action.GetPrefab(dstManager);
        var reqPrefab = dstManager.GetComponentData<Requirement>(action).Prefab;
        var reqInstance = dstManager.Instantiate(reqPrefab);
        dstManager.AddComponentData(reqInstance, new Dealer() { Entity = _checkOn.Entity });
        dstManager.AddComponentData(entity, new RequirementCheckUI() { BlockingGroup = _blockingGroup, Requirement = reqInstance });
    }
}
