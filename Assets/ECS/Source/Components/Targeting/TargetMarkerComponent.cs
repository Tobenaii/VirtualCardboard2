using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public interface ITargetMarker
{
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }
}

public struct TargetMarker : ITargetMarker, IComponentData
{
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }
}

public class TargetMarkerComponent : ComponentAuthoringBase
{
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<TargetMarker>(entity);
    }
}
