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

public abstract class AttributeAuthoring<T> : ComponentAuthoring<T> where T : struct, IAttribute
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

public abstract class StatAuthoring<T> : ComponentAuthoring<T> where T : struct, IStat
{
    [SerializeField] private float _maxValue;

    protected override T AuthorComponent(World world)
    {
        return new T() { BaseValue = _maxValue, CurrentValue = _maxValue, MaxValue = _maxValue };
    }
}

public interface IChargeStat : IComponentData
{
    public int Charges { get; set; }
    public float ChargeTime { get; set; }
    public int MaxCharges { get; set; }
    public int MaxPool { get; set; }
    public int Pool { get; set; }
    public float ChargeTimer { get; set; }
}

public abstract class ChargeStatAuthoring<T> : ComponentAuthoring<T> where T : struct, IChargeStat
{
    [SerializeField] private float _chargeTime;
    [SerializeField] private int _maxCharges;
    [SerializeField] private int _chargePool;

    protected override T AuthorComponent(World world)
    {
        return new T() { Charges = 0, ChargeTime = _chargeTime, MaxCharges = _maxCharges,
                        MaxPool = _chargePool, Pool = _chargePool, ChargeTimer = 0 };
    }
}

public interface IPrefabCollection : IBufferElementData
{
    public Entity Entity { get; set; }
}

public abstract class PrefabCollectionAuthoring<T> : BufferComponentAuthoring<T> where T : struct, IPrefabCollection
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

public interface ICollectionContainer
{
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
}

//public abstract class PrefabCollectionContainerAuthoring<T, V> : ComponentAuthoring<T> where T : struct, IPrefabCollectionContainer<V> where V : struct, IPrefabCollection
//{
//    [SerializeField] private int _maxCount;
//    [SerializeField] private bool _debugList;
//    [ShowIf("@_debugList")]
//    [SerializeField] private List<ModEntity> _entities;

//    protected override T AuthorComponent(World world)
//    {
//        return new T() { CurrentCount = _debugList ? _entities.Count : _maxCount, MaxCount = _maxCount };
//    }

//    public override void AuthorDependencies(Entity entity, EntityManager dstManager)
//    {
//        var instances = new NativeArray<V>(_entities.Count, Allocator.Temp);
//        if (_debugList)
//        {
//            int i = 0;
//            foreach (var modEntity in _entities)
//            {
//                var instance = new V();
//                instance.Entity = modEntity.GetPrefab(dstManager);
//                instances[i] = instance;
//                i++;
//            }
//        }
//        var buffer = dstManager.AddBuffer<V>(entity);
//        buffer.AddRange(instances);
//    }
//}