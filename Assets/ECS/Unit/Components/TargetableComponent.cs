using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Targetable : IComponentAuthoring<Targetable, TargetableComponent>
{
    public float3 localMarkerOffset;
    public float markerScale;
}

public class TargetableComponent : UnitComponentAuthoring<Targetable>
{
    [SerializeField] private Vector3 _localMarkerOffset;
    [SerializeField] private float _markerScale;

    protected override Targetable AuthorComponent(World world)
    {
        return new Targetable() { localMarkerOffset = _localMarkerOffset, markerScale = _markerScale };
    }
}
