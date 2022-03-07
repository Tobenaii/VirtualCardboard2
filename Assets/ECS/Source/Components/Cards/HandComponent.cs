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

public class HandComponent : ComponentAuthoring<Hand>
{
    public override void AuthorDependencies(Entity entity, EntityManager dstManager)
    {
        dstManager.AddBuffer<HandCard>(entity);
    }

    protected override Hand AuthorComponent(World world)
    {
        return new Hand();
    }
}
