using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[InternalBufferCapacity(8)]
public struct Deck : IPrefabCollection
{
    public Entity Entity { get; set; }
}

public class DeckComponent : PrefabCollectionAuthoring<Deck>
{
}