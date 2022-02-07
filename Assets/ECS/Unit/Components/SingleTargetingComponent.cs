using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public interface ITargeting : IComponentData 
{ 
    public float3 Position { get; set; }
    public float Scale { get; set; }
}

public struct SingleTargeting : ITargeting
{
    public bool isTargeting;
    public int targetIndex;
    public Entity target;
    public float3 Position { get; set; }
    public float Scale { get; set; }
}

public class SingleTargetingComponent : UnitComponentAuthoring<SingleTargeting>
{
    protected override SingleTargeting AuthorComponent(World world)
    {
        return new SingleTargeting() { isTargeting = true };
    }
}
