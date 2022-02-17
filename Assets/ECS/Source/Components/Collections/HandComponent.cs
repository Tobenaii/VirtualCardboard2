using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct Hand : IPrefabCollectionContainer, IComponentData
{
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
}

[InternalBufferCapacity(5)]
public struct HandCards : IPrefabCollection, IBufferElementData
{
    public Entity Entity { get; set; }
}

public class HandComponent : PrefabCollectionContainerAuthoring<Hand, HandCards>
{
}
