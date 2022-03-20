using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct Deck : IComponentData
{
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
}

[InternalBufferCapacity(30)]
public struct DeckCard : IBufferElementData
{
    public Entity Entity { get; set; }
}

public class DeckComponent : ComponentAuthoringBase
{
    [SerializeField] private List<EntityData> _entities = new List<EntityData>();

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new Deck() { CurrentCount = _entities.Count, MaxCount = _entities.Count });
        var array = new NativeArray<DeckCard>(_entities.Count, Allocator.Temp);
        for (int i = 0; i < _entities.Count; i++)
        {
            var instance = new DeckCard();
            instance.Entity = _entities[i].GetPrefab(dstManager);
            array[i] = instance;
        }
        var buffer = dstManager.AddBuffer<DeckCard>(entity);
        buffer.AddRange(array);
    }
}
