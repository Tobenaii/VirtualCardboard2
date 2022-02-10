using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[InternalBufferCapacity(8)]
public struct DeckCard : IPrefabCollection
{
    public Entity Entity { get; set; }
}

public class DeckCardComponent : PrefabCollectionAuthoring<DeckCard>
{
}