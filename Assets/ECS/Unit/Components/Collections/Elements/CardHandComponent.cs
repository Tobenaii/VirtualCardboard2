using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[InternalBufferCapacity(8)]
public struct HandCard : IPrefabCollection
{
    public Entity Entity { get; set; }
}

public class CardHandComponent : PrefabCollectionAuthoring<HandCard>
{
}
