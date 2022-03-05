using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IStatusEffectData
{
    public int Type { get; set; }
    public bool Active { get; set; }
}

[InternalBufferCapacity(10)]
[System.Serializable]
public struct StatusEffect : IStatusEffectData, IBufferElementData
{
    public int Type { get; set; }
    //TODO: Change this to count so that we could have multiple of the same status effect on the same target
    public bool Active { get; set; }
}

public class StatusEffectsComponent : BufferComponentAuthoring<StatusEffect>
{
    [SerializeField] private StatusEffectGroup _group;

    protected override NativeArray<StatusEffect> AuthorComponent(World world)
    {
        var array = new NativeArray<StatusEffect>(_group.Count, Allocator.Temp);
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = new StatusEffect() { Type = i, Active = false };
        }
        return array;
    }
}