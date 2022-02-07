using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct SingleTargeting : IComponentData
{
    public bool isTargeting;
    public int targetIndex;
    public Entity target;
    public float3 position;
    public float scale;
}

public class SingleTargetingComponent : UnitComponentAuthoring<SingleTargeting>
{
    protected override SingleTargeting AuthorComponent(World world)
    {
        return new SingleTargeting() { isTargeting = true };
    }
}
