using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[InternalBufferCapacity(2)]
public struct Stat : ITypeBufferElementEvent<Stat.Type>, IBufferElementData
{
    public enum Type { Health, ATB }
    public Type StatType;
    public float CurrentValue;
    public float MaxValue;


    Type ITypeBufferElementEvent<Type>.Type => StatType;
}

public class StatComponent : BufferComponentAuthoring<Stat>
{
    [System.Serializable]
    private class StatAuthoring
    {
        [SerializeField] private Stat.Type _type;
        [SerializeField] private float _initialValue;
        [SerializeField] private float _maxValue;

        public Stat.Type Type => _type;
        public float InitialValue => _initialValue;
        public float MaxValue => _maxValue;
    }
    [SerializeField] private List<StatAuthoring> _stats = new List<StatAuthoring>();

    protected override NativeArray<Stat> AuthorComponent(World world)
    {
        var array = new NativeArray<Stat>(Enum.GetNames(typeof(Stat.Type)).Length, Allocator.Temp);
        int index = 0;
        for (int i = 0; i < array.Length; i++)
        {
            var authoring = _stats[index];
            if ((int)authoring.Type == i)
            {
                array[i] = new Stat() { StatType = authoring.Type, CurrentValue = authoring.InitialValue,
                    MaxValue = authoring.MaxValue };
            }
            else
                array[i] = new Stat() { StatType = (Stat.Type)i };
        }
        return array;
    }
}
