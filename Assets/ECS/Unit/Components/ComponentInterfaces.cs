using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IAttribute : IComponentData
{
    public float Value { get; set; }
}

public abstract class AttributeAuthoring<T> : UnitComponentAuthoring<T> where T : struct, IAttribute
{
    [SerializeField] private float _value;

    protected override T AuthorComponent(World world)
    {
        return new T() { Value = _value };
    }
}

public interface IStat : IComponentData
{
    public float BaseValue { get; set; }
    public float CurrentValue { get; set; }
    public float MaxValue { get; set; }
}

public abstract class StatAuthoring<T> : UnitComponentAuthoring<T> where T : struct, IStat
{
    [SerializeField] private float _maxValue;

    protected override T AuthorComponent(World world)
    {
        return new T() { BaseValue = _maxValue, CurrentValue = _maxValue, MaxValue = _maxValue };
    }
}

public interface IPrefabCollection : IBufferElementData
{
    public Entity Entity { get; set; }
}

public abstract class PrefabCollectionAuthoring<T> : UnitBufferComponentAuthoring<T> where T : struct, IPrefabCollection
{
    [SerializeField] private List<ModEntity> _prefabs;

    protected override NativeArray<T> AuthorComponent(World world)
    {
        var array = new NativeArray<T>(_prefabs.Count, Allocator.Temp);
        for (int i = 0; i < _prefabs.Count; i++)
        {
            array[i] = new T() { Entity = _prefabs[i].GetPrefab(world.EntityManager) };
        }
        return array;
    }
}

public interface ICollectionContainer : IComponentData
{
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
}

public interface IPrefabCollectionContainer<T> : ICollectionContainer where T : struct, IPrefabCollection
{
}

public abstract class PrefabCollectionContainerAuthoring<T, V, U> : UnitComponentAuthoring<T> where T : struct, IPrefabCollectionContainer<V> where V : struct, IPrefabCollection where U : ModEntity
{
    [SerializeField] private int _maxCount;
    [SerializeField] private bool _debugList;
    [ShowIf("@_debugList")]
    [SerializeField] private List<U> _entities;

    protected override T AuthorComponent(World world)
    {
        return new T() { CurrentCount = _debugList ? _entities.Count : _maxCount, MaxCount = _maxCount };
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
                instance.Entity = modEntity.GetPrefab(dstManager);
                instances[i] = instance;
                i++;
            }
        }
        var buffer = dstManager.AddBuffer<V>(entity);
        buffer.AddRange(instances);
    }
}