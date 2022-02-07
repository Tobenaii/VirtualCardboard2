using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IEntityBuffer : IBufferElementData
{
    public Entity Entity { get; set; }
}

[InternalBufferCapacity(8)]
public struct CardHand : IEntityBuffer
{
    public Entity Entity { get; set; }
}

public class CardHandComponent : UnitBufferComponentAuthoring<CardHand>
{
    [SerializeField] private List<CardData> _cardData;

    protected override NativeArray<CardHand> AuthorComponent(World world)
    {
        var array = new NativeArray<CardHand>(_cardData.Count, Allocator.Temp);
        for (int i = 0; i < _cardData.Count; i++)
        {
            array[i] = new CardHand() { Entity = _cardData[i].GetPrefab(world.EntityManager) };
        }
        return array;
    }
}
