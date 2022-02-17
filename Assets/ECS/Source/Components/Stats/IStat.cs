using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public interface IStat
{
    public int BaseValue { get; set; }
    public int CurrentValue { get; set; }
    public int MaxValue { get; set; }
}

public abstract class StatAuthoring<T> : ComponentAuthoring<T> where T : unmanaged, IStat, IComponentData
{
    [SerializeField] private int _initialValue;
    [SerializeField] private int _maxValue;

    private void OnValidate()
    {
        if (_initialValue > _maxValue)
            _initialValue = _maxValue;
    }

    protected override T AuthorComponent(World world)
    {
        return new T() { BaseValue = _maxValue, CurrentValue = _initialValue, MaxValue = _maxValue };
    }
}

