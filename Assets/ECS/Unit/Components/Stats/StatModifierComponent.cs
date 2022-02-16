using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[InternalBufferCapacity(10)]
public struct StatModifier : IBufferElementData
{
    public enum Type { Damage = -Stat.Type.Health, Heal = +Stat.Type.Health}
    public Type ModType;
    public float Amount;
}

public class StatModifierComponent : BufferComponentAuthoring<StatModifier>
{
    protected override NativeArray<StatModifier> AuthorComponent(World world)
    {
        return new NativeArray<StatModifier>();
    }
}
