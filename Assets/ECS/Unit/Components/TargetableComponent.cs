using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Targetable : IComponentData
{
    public float3 offset;
    public float scale;
}

public class TargetableComponent : ComponentAuthoring<Targetable>
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _markerScale;

    protected override Targetable AuthorComponent(World world)
    {
        return new Targetable() { offset = _offset, scale = _markerScale };
    }
}
