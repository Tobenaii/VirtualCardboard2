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

public class DeckComponent : ComponentAuthoring<Deck>
{
    [SerializeField] private List<ModEntity> _entities = new List<ModEntity>();

    protected override Deck AuthorComponent(World world)
    {
        return new Deck() { CurrentCount = _entities.Count, MaxCount = _entities.Count };
    }

    public override void AuthorDependencies(Entity entity, EntityManager dstManager)
    {
        var instances = new NativeArray<DeckCard>(_entities.Count, Allocator.Temp);
        int i = 0;
        foreach (var modEntity in _entities)
        {
            var instance = new DeckCard();
            instance.Entity = modEntity.GetPrefab(dstManager, modEntity.name);
            instances[i] = instance;
            i++;
        }
        var buffer = dstManager.AddBuffer<DeckCard>(entity);
        buffer.AddRange(instances);
    }
}
