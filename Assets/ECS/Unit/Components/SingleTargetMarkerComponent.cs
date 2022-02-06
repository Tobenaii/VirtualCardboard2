using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct SingleTargetMarker : IComponentAuthoring<SingleTargetMarker, SingleTargetMarkerComponent>
{
    public float3 position;
    public float3 scale;
    public quaternion rotation;

    public float3 positionVelocity;
    public float3 scaleVelocity;
}

public class SingleTargetMarkerComponent : UnitComponentAuthoring<SingleTargetMarker>
{
    protected override SingleTargetMarker AuthorComponent(World world)
    {
        return new SingleTargetMarker();
    }
}
