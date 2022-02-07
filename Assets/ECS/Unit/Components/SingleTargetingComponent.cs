using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public interface ISingleTargeting : IComponentData 
{ 
    public float3 Position { get; set; }
    public float Scale { get; set; }
}

public struct SingleTarget : ISingleTargeting
{
    public bool isTargeting;
    public int targetIndex;
    public Entity target;
    public float3 Position { get; set; }
    public float Scale { get; set; }
}

public class SingleTargetingComponent : UnitComponentAuthoring<SingleTarget>
{
    protected override SingleTarget AuthorComponent(World world)
    {
        return new SingleTarget() { isTargeting = true };
    }
}
