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

public class TargetableComponent : ComponentAuthoringBase
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector3 _scale;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new Targetable() { Position = _position, Scale = _scale });
    }
}
