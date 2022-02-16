using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[InternalBufferCapacity(2)]
public struct ApplyStatModifier : IBufferElementData
{
    public StatModifier.Type Type;
    public float Amount;
}

public class ApplyStatModifierComponent : BufferComponentAuthoring<ApplyStatModifier>
{
    [System.Serializable]
    private class Authoring
    {
        [SerializeField] private StatModifier.Type _type;
        [SerializeField] private float _amount;

        public StatModifier.Type Type => _type;
        public float Amount => _amount;
    }
    [SerializeField] private List<Authoring> _modifiers = new List<Authoring>();
    protected override NativeArray<ApplyStatModifier> AuthorComponent(World world)
    {
        var array = new NativeArray<ApplyStatModifier>(_modifiers.Count, Allocator.Temp);
        int index = 0;
        foreach (var modifier in _modifiers)
        {
            array[index] = new ApplyStatModifier() { Type = modifier.Type, Amount = modifier.Amount };
            index++;
        }
        return array;
    }
}
