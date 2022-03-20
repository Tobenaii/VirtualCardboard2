using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Target : IComponentData
{
    public Entity Entity { get; set; }
}

public class TargetComponent : ComponentAuthoringBase
{

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<Target>(entity);
    }
}
