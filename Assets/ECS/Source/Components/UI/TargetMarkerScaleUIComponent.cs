using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TargetMarkerScaleUI : IComponentData, ISmoothDamp<Vector3>
{
    public Entity Target { get; set; }
    public Transform Transform { get; set; }
    public float SmoothTime { get; set; }

    private Vector3 _velocity;

    public Vector3 SmoothDamp(Vector3 current, Vector3 target)
    {
        return Vector3.SmoothDamp(current, target, ref _velocity, SmoothTime);
    }
}

public class TargetMarkerScaleUIComponent : ManagedComponentAuthoring<TargetMarkerScaleUI>
{
    [SerializeField] private EntityRef _target;
    [SerializeField] private Transform _transform;
    [SerializeField] private float _smoothTime;
    
    protected override TargetMarkerScaleUI AuthorComponent(World world)
    {
        return new TargetMarkerScaleUI() { Target = _target.Entity, SmoothTime = _smoothTime, Transform = _transform };
    }
}
