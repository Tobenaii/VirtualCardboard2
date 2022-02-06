using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct SingleTarget : IComponentAuthoring<SingleTarget, SingleTargetComponent>
{
    public bool isTargeting;
    public int targetIndex;
    public Entity target;
}

public class SingleTargetComponent : UnitComponentAuthoring<SingleTarget>
{
    protected override SingleTarget AuthorComponent(World world)
    {
        return new SingleTarget() { isTargeting = true };
    }
}
