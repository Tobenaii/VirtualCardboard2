using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct PerformActionOnSpawn : IComponentData
{
    public Entity ActionPrefab { get; set; }
}

public class PerformActionOnSpawnComponent : ComponentAuthoringBase
{
    [SerializeField] private EntityData _action;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        _action.Convert(entity, dstManager);
        dstManager.AddComponent<PerformActionOnSpawn>(entity);
    }
}
