using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct DeckCard : ITypeBufferElementEvent<DeckCard.State>, IBufferElementData
{
    public State Type => CardState;
    public enum State { Added, Destroyed }
    public State CardState { get; set; }
    public Entity Entity { get; set; }
}

public class DeckComponent : BufferComponentAuthoring<DeckCard>
{
    [SerializeField] private List<ModEntity> _entities;
    protected override NativeArray<DeckCard> AuthorComponent(World world)
    {
        var array = new NativeArray<DeckCard>(_entities.Count, Allocator.Temp);
        int i = 0;
        foreach (var entity in _entities)
        {
            array[i] = new DeckCard() { Entity = entity.GetPrefab(world.EntityManager) };
        }
        return array;
    }
}
