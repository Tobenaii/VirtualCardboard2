using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class TargetMarkerPositionUI : IComponentData, ISmoothDamp<Vector3>
{
    public Transform Transform { get; set; }
    public float SmoothTime { get; set; }

    private Vector3 _velocity;

    public Vector3 SmoothDamp(Vector3 current, Vector3 target)
    {
        return Vector3.SmoothDamp(current, target, ref _velocity, SmoothTime);
    }
}

[MovedFrom(true, sourceClassName: "TargetMarkerUIComponent")]
public class TargetMarkerPositionUIComponent : ComponentAuthoringBase
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _smoothTime;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new TargetMarkerPositionUI() { SmoothTime = _smoothTime, Transform = _transform });
    }
}
