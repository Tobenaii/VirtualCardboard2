using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IPrefabCollection
{
    public Entity Entity { get; set; }
}

public interface ICollectionContainer
{
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
}

public interface IPrefabCollectionContainer : ICollectionContainer
{
}

public abstract class PrefabCollectionContainerAuthoring<T, V> : ComponentAuthoring<T> where T : struct, IComponentData, IPrefabCollectionContainer where V : struct, IPrefabCollection, IBufferElementData
{
    [SerializeField] private bool _debugList;
    [ShowIf("@_debugList")]
    [SerializeField] private List<ModEntity> _entities = new List<ModEntity>();

    protected override T AuthorComponent(World world)
    {
        return new T() { CurrentCount = _entities.Count, MaxCount = _entities.Count };
    }

    public override void AuthorDependencies(Entity entity, EntityManager dstManager)
    {
        var instances = new NativeArray<V>(_entities.Count, Allocator.Temp);
        if (_debugList)
        {
            int i = 0;
            foreach (var modEntity in _entities)
            {
                var instance = new V();
                instance.Entity = modEntity.GetPrefab(dstManager, modEntity.name);
                instances[i] = instance;
                i++;
            }
        }
        var buffer = dstManager.AddBuffer<V>(entity);
        buffer.AddRange(instances);
    }
}