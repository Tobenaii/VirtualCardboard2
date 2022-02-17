using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public interface ITargetable
{
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }
}

public struct Targetable : ITargetable, IComponentData
{
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }
}

public class TargetableComponent : ComponentAuthoring<Targetable>
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector3 _scale;
    protected override Targetable AuthorComponent(World world)
    {
        return new Targetable() { Position = _position, Scale = _scale };
    }
}
