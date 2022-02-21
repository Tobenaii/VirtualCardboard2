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

    protected override Target AuthorComponent(World world)
    {
        return new Target();
    }
}
