using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct Hand : IComponentData
{
    public int CurrentCount { get; set; }
}

[InternalBufferCapacity(5)]
public struct HandCard : IBufferElementData
{
    public Entity Entity { get; set; }
}

public class HandComponent : ComponentAuthoringBase
{
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<Hand>(entity);
        dstManager.AddBuffer<HandCard>(entity);
    }
}
