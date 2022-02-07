using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IAttribute : IComponentData
{
    public float Value { get; set; }
}

public class AttributeAuthoring<T> : UnitComponentAuthoring<T> where T : struct, IAttribute
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

public class StatAuthoring<T> : UnitComponentAuthoring<T> where T : struct, IStat
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

public class PrefabCollectionAuthoring<T> : UnitBufferComponentAuthoring<T> where T : struct, IPrefabCollection
{
    [SerializeField] private List<ArchetypePrefab> _prefabs;

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
