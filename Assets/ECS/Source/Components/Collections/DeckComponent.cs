using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Deck : IPrefabCollectionContainer, IComponentData
{
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
}

[InternalBufferCapacity(30)]
public struct DeckCard : IPrefabCollection, IBufferElementData
{
    public Entity Entity { get; set; }
}

public class DeckComponent : PrefabCollectionContainerAuthoring<Deck, DeckCard>
{
}
