using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public interface ITarget
{
    public Entity Dealer { get; set; }
    public Entity TargetEntity { get; set; }
}

public struct Target : ITarget, IComponentData
{
    public Entity Dealer { get; set; }
    public Entity TargetEntity { get; set; }
    public bool HasTarget { get; set; }
}

public class TargetComponent : ComponentAuthoring<Target>
{
    [SerializeField] private bool _hasInitialTarget;
    [ShowIf("@_hasInitialTarget")]
    [SerializeField] private EntityRef _target;
    protected override Target AuthorComponent(World world)
    {
        if (_hasInitialTarget)
            return new Target() { TargetEntity = _target.Entity, HasTarget = true };
        else
            return new Target();
    }
}
