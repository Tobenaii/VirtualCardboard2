using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[InternalBufferCapacity(3)]
public struct Cards : ITypeBufferElementEvent<Cards.Collection>, IBufferElementData, ICollectionContainer
{
    public enum Collection { Deck, Hand, Graveyard }
    public Collection Type { get; set; }

    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
}

public class CardHolderComponent : BufferComponentAuthoring<Cards>
{
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        base.AuthorComponent(entity, dstManager);

    }
    protected override NativeArray<Cards> AuthorComponent(World world)
    {
        var enumLength = Enum.GetNames(typeof(Cards.Collection)).Length;
        var array = new NativeArray<Cards>(enumLength, Allocator.Temp);
        for (int i = 0; i < enumLength; i++)
        {
            array[i] = new Cards() { Type = (Cards.Collection)i };
        }
        return array;
    }
}
